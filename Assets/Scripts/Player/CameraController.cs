using UnityEngine;
using Game.Player;
using UnityEditor;
using UnityEngine.Rendering;
using Cysharp.Threading.Tasks;
using System;

namespace Game.Player
{
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField] PlayerController _player;
        [SerializeField] bool _isChangeOffset;
        [SerializeField] Vector3 _offset;
        Vector3 _currentPos;

        private void OnValidate()
        {
            if (_isChangeOffset && _player != null) ChangeOffset();
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Initialize();
        }
        // Update is called once per frame
        void Update()
        {

        }
        private void LateUpdate()
        {
            if (_player == null) return;
            CameraMove();
        }
        void CameraMove()
        {
            _currentPos = _player.transform.position + _offset;
            transform.position = _currentPos;
        }

        void Initialize()
        {
            if (_player != null) _player.OnRotatePlayerAsync = RotateCameraAsync;
        }
        // エディタ表示が更新されるたびに呼ばれる特殊な関数
        private void OnDrawGizmos()
        {
            // ゲーム再生中でなく、フラグがONで、プレイヤーがいる場合のみリアルタイム計算
            if (!Application.isPlaying && _isChangeOffset && _player != null)
            {
                ChangeOffset();
            }
        }

        //Playerが回転したときに呼ばれる関数
        async UniTask RotateCameraAsync(Quaternion targetRot, float rotateSpeed)
        {
            try
            {
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: this.GetCancellationTokenOnDestroy());
            }
            catch (OperationCanceledException) { return; }
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRot,
                rotateSpeed * Time.deltaTime
                );
        }
        void ChangeOffset()
        {
#if UNITY_EDITOR
            // 変化があったときだけ記録しないと重くなるので、値が変わるかチェック
            Vector3 targetOffset = _player.transform.position - transform.position;
            if (_offset != targetOffset)
            {
                Undo.RecordObject(this, "Change Camera Offset");
                _offset = targetOffset;
                EditorUtility.SetDirty(this);
            }
#endif
        }
    }

}



