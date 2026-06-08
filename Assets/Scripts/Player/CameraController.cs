using UnityEngine;
using Game.Player;
using UnityEditor;
using UnityEngine.Rendering;
using Cysharp.Threading.Tasks;

namespace Game.Player
{
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField] PlayerController _player;
        [SerializeField] bool _isChangeOffset;
        [SerializeField] Vector3 _offset;
        Vector3 _currentPos;
        Vector3 _currentOffset;
        Vector3 _baseOffset;
        Quaternion _targetRot;
        float _rotateSpeed;

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
            // プレイヤー回転後の理想Offsetを計算
            var targetOffset = _targetRot * _baseOffset;

            // 現在のOffsetを目標Offsetへ徐々に補間
            _currentOffset = Vector3.RotateTowards(
                _currentOffset,
                targetOffset,
                //ラジアン/フレーム
                _rotateSpeed * Mathf.Deg2Rad * Time.deltaTime,
                0f
            );

            // プレイヤーの後方にカメラを配置
            _currentPos = _player.transform.position - _currentOffset;
            transform.position = _currentPos;

            // カメラの回転も徐々に補間
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                _targetRot,
                _rotateSpeed * Time.deltaTime
            );
        }

        void Initialize()
        {
            if (_player == null) return;

            _targetRot = _player.transform.rotation;
            _baseOffset = Quaternion.Inverse(_targetRot) * _offset;
            _currentOffset = _offset;
            _player.OnRotatePlayerAsync = RotateCameraAsync;
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
        UniTask RotateCameraAsync(Quaternion targetRot, float rotateSpeed)
        {
            _targetRot = targetRot;
            _rotateSpeed = rotateSpeed;
            return UniTask.CompletedTask;
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



