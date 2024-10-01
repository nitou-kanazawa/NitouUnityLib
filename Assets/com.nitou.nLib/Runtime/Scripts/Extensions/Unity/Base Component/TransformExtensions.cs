using UnityEngine;

// [参考]
//  コガネブログ: Transform型の位置や回転角、サイズの設定を楽にする https://baba-s.hatenablog.com/entry/2014/02/28/000000
//  _:  Transformにリセット処理を追加してみる https://ookumaneko.wordpress.com/2015/10/01/unity%E3%83%A1%E3%83%A2-transform%E3%81%AB%E3%83%AA%E3%82%BB%E3%83%83%E3%83%88%E5%87%A6%E7%90%86%E3%82%92%E8%BF%BD%E5%8A%A0%E3%81%97%E3%81%A6%E3%81%BF%E3%82%8B/#:~:text=%E3%83%AF%E3%83%BC%E3%83%AB%E3%83%89%E3%82%92%E3%83%AA%E3%82%BB%E3%83%83%E3%83%88%E3%81%97%E3%81%9F%E3%81%84%E6%99%82,%E5%80%A4%E3%82%92%E3%83%AA%E3%82%BB%E3%83%83%E3%83%88%E5%87%BA%E6%9D%A5%E3%81%BE%E3%81%99%E3%80%82
//  github: BreadcrumbsUnityCsReference/Editor/Mono/GameObjectUtility.bindings.cs https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/GameObjectUtility.bindings.cs#L75

namespace nitou {

    /// <summary>
    /// GameObjectの拡張メソッドクラス
    /// </summary>
    public static partial class TransformExtensions {

        /// ----------------------------------------------------------------------------
        #region 位置の設定

        /// <summary>
        /// X座標を設定する拡張メソッド
        /// </summary>
        public static void SetPositionX(this Transform self, float x) =>
            self.position = new Vector3(x, self.position.y, self.position.z);

        /// <summary>
        /// Y座標を設定する拡張メソッド
        /// </summary>
        public static void SetPositionY(this Transform self, float y) =>
            self.position = new Vector3(self.position.x, y, self.position.z);

        /// <summary>
        /// Z座標を設定する拡張メソッド
        /// </summary>
        public static void SetPositionZ(this Transform self, float z) =>
            self.position = new Vector3(self.position.x, self.position.y, z);

        /// <summary>
        /// ローカルのX座標を設定する拡張メソッド
        /// </summary>
        public static void SetLocalPositionX(this Transform self, float x) =>
            self.localPosition = new Vector3(x, self.localPosition.y, self.localPosition.z);

        /// <summary>
        /// ローカルのY座標を設定する拡張メソッド
        /// </summary>
        public static void SetLocalPositionY(this Transform self, float y) =>
            self.localPosition = new Vector3(self.localPosition.x, y, self.localPosition.z);

        /// <summary>
        /// ローカルのZ座標を設定する拡張メソッド
        /// </summary>
        public static void SetLocalPositionZ(this Transform self, float z) =>
            self.localPosition = new Vector3(self.localPosition.x, self.localPosition.y, z);


        /// <summary>
        /// X座標に加算する拡張メソッド
        /// </summary>
        public static void AddPositionX(this Transform self, float x) =>
            self.SetPositionX(x + self.position.x);

        /// <summary>
        /// Y座標に加算する拡張メソッド
        /// </summary>
        public static void AddPositionY(this Transform self, float y) =>
            self.SetPositionY(y + self.position.y);

        /// <summary>
        /// Z座標に加算する拡張メソッド
        /// </summary>
        public static void AddPositionZ(this Transform self, float z) =>
            self.SetPositionZ(z + self.position.z);

        /// <summary>
        /// ローカルのX座標に加算する拡張メソッド
        /// </summary>
        public static void AddLocalPositionX(this Transform self, float x) =>
            self.SetLocalPositionX(x + self.localPosition.x);

        /// <summary>
        /// ローカルのY座標に加算する拡張メソッド
        /// </summary>
        public static void AddLocalPositionY(this Transform self, float y) =>
            self.SetLocalPositionY(y + self.localPosition.y);

        /// <summary>
        /// ローカルのZ座標に加算する拡張メソッド
        /// </summary>
        public static void AddLocalPositionZ(this Transform self, float z) =>
            self.SetLocalPositionZ(z + self.localPosition.z);
        #endregion


        /// ----------------------------------------------------------------------------
        #region 角度の設定

        /// <summary>
        /// X軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleX(this Transform self, float x) =>
            self.eulerAngles = new Vector3(x, self.eulerAngles.y, self.eulerAngles.z);

        /// <summary>
        /// Y軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleY(this Transform self, float y) =>
            self.eulerAngles = new Vector3(self.eulerAngles.x, y, self.eulerAngles.z);

        /// <summary>
        /// Z軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleZ(this Transform self, float z) =>
            self.eulerAngles = new Vector3(self.eulerAngles.x, self.eulerAngles.y, z);

        /// <summary>
        /// ローカルのX軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleX(this Transform self, float x) =>
            self.localEulerAngles = new Vector3(x, self.localEulerAngles.y, self.localEulerAngles.z);

        /// <summary>
        /// ローカルのY軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleY(this Transform self, float y) =>
            self.localEulerAngles = new Vector3(self.localEulerAngles.x, y, self.localEulerAngles.z);

        /// <summary>
        /// ローカルのZ軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleZ(this Transform self, float z) =>
            self.localEulerAngles = new Vector3(self.localEulerAngles.x, self.localEulerAngles.y, z);


        /// <summary>
        /// X軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleX(this Transform self, float x) =>
            self.SetEulerAngleX(self.eulerAngles.x + x);

        /// <summary>
        /// Y軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleY(this Transform self, float y) =>
            self.SetEulerAngleY(self.eulerAngles.y + y);

        /// <summary>
        /// Z軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleZ(this Transform self, float z) =>
            self.SetEulerAngleZ(self.eulerAngles.z + z);

        /// <summary>
        /// ローカルのX軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleX(this Transform self, float x) =>
            self.SetLocalEulerAngleX(self.localEulerAngles.x + x);

        /// <summary>
        /// ローカルのY軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleY(this Transform self, float y) =>
            self.SetLocalEulerAngleY(self.localEulerAngles.y + y);

        /// <summary>
        /// ローカルのX軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleZ(this Transform self, float z) =>
            self.SetLocalEulerAngleZ(self.localEulerAngles.z + z);
        #endregion


        /// ----------------------------------------------------------------------------
        #region スケールの設定

        /// <summary>
        /// X軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleX(this Transform self, float x) =>
            self.localScale = new Vector3(x, self.localScale.y, self.localScale.z);

        /// <summary>
        /// Y軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleY(this Transform self, float y) =>
            self.localScale = new Vector3(self.localScale.x, y, self.localScale.z);

        /// <summary>
        /// Z軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleZ(this Transform self, float z) =>
            self.localScale = new Vector3(self.localScale.x, self.localScale.y, z);


        /// <summary>
        /// X軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleX(this Transform self, float x) =>
            self.SetLocalScaleX(self.localScale.x + x);

        /// <summary>
        /// Y軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleY(this Transform self, float y) =>
            self.SetLocalScaleY(self.localScale.y + y);

        /// <summary>
        /// Z軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleZ(this Transform self, float z) =>
            self.SetLocalScaleZ(self.localScale.z + z);
        #endregion


        /// ----------------------------------------------------------------------------
        #region 初期化

        /// <summary>
        /// ローカルの座標，回転，スケールを初期化する拡張メソッド
        /// </summary>
        public static void ResetLocal(this Transform self) {
            self.ResetLocalPositionAndRotation();
            self.localScale = Vector3.one;
        }

        /// <summary>
        /// 座標，回転，スケールを初期化する拡張メソッド
        /// </summary>
        public static void ResetWorld(this Transform self) {
            self.ResetWorldPositionAndRotation();
            self.localScale = Vector3.one;
        }

        /// <summary>
        /// ローカルの座標，回転を初期化する拡張メソッド
        /// </summary>
        public static void ResetLocalPositionAndRotation(this Transform self) {
#if UNITY_2021_3_OR_NEWER
            self.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
#else
            self.localPosition = Vector3.zero;
            self.localRotation = Quaternion.identity;
#endif
        }

        /// <summary>
        /// ローカルの座標，回転を初期化する拡張メソッド
        /// </summary>
        public static void ResetWorldPositionAndRotation(this Transform self) {
#if UNITY_2021_3_OR_NEWER
            self.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
#else
            self.position = Vector3.zero;
            self.rotation = Quaternion.identity;
#endif
        }
        #endregion


        /// ----------------------------------------------------------------------------
        // その他

        /// <summary>
        /// 指定した方向を向かせる拡張メソッド
        /// </summary>
        public static void LookDirection(this Transform self, Vector3 direction) {
            self.LookAt(self.position + direction);
        }

        /// <summary>
        /// 全ての子要素を削除する拡張メソッド
        /// </summary>
        public static void DestroyAllChildren(this Transform self) {
            foreach (Transform child in self) {
                GameObject.Destroy(child.gameObject);
            }
        }
    }



    /// <summary>
    /// GameObject型の拡張メソッドを管理するクラス
    /// </summary>
    public static partial class GameObjectExtensions {

        /// ----------------------------------------------------------------------------
#region 位置の設定

        /// <summary>
        /// 位置を設定します
        /// </summary>
        public static void SetPosition(this GameObject self, Vector3 position) {
            self.transform.position = position;
        }

        /// <summary>
        /// X座標を設定します
        /// </summary>
        public static void SetPositionX(this GameObject self, float x) {
            self.transform.SetPositionX(x);
        }

        /// <summary>
        /// Y座標を設定します
        /// </summary>
        public static void SetPositionY(this GameObject self, float y) {
            self.transform.SetPositionY(y);
        }

        /// <summary>
        /// Z座標を設定します
        /// </summary>
        public static void SetPositionZ(this GameObject self, float z) {
            self.transform.SetPositionZ(z);
        }

        /// <summary>
        /// ローカル座標系の位置を設定します
        /// </summary>
        public static void SetLocalPosition(this GameObject self, Vector3 localPosition) {
            self.transform.localPosition = localPosition;
        }

        /// <summary>
        /// ローカル座標系のX座標を設定します
        /// </summary>
        public static void SetLocalPositionX(this GameObject self, float x) {
            self.transform.SetLocalPositionX(x);
        }

        /// <summary>
        /// ローカル座標系のY座標を設定します
        /// </summary>
        public static void SetLocalPositionY(this GameObject self, float y) {
            self.transform.SetLocalPositionY(y);
        }

        /// <summary>
        /// ローカルのZ座標を設定します
        /// </summary>
        public static void SetLocalPositionZ(this GameObject self, float z) {
            self.transform.SetLocalPositionZ(z);
        }


        /// <summary>
        /// X座標に加算します
        /// </summary>
        public static void AddPositionX(this GameObject self, float x) {
            self.transform.AddPositionX(x);
        }

        /// <summary>
        /// Y座標に加算します
        /// </summary>
        public static void AddPositionY(this GameObject self, float y) {
            self.transform.AddPositionY(y);
        }

        /// <summary>
        /// Z座標に加算します
        /// </summary>
        public static void AddPositionZ(this GameObject self, float z) {
            self.transform.AddPositionZ(z);
        }

        /// <summary>
        /// ローカル座標系のX座標に加算します
        /// </summary>
        public static void AddLocalPositionX(this GameObject self, float x) {
            self.transform.AddLocalPositionX(x);
        }

        /// <summary>
        /// ローカル座標系のY座標に加算します
        /// </summary>
        public static void AddLocalPositionY(this GameObject self, float y) {
            self.transform.AddLocalPositionY(y);
        }

        /// <summary>
        /// ローカル座標系のZ座標に加算します
        /// </summary>
        public static void AddLocalPositionZ(this GameObject self, float z) {
            self.transform.AddLocalPositionZ(z);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 角度の設定

        /// <summary>
        /// 回転角を設定します
        /// </summary>
        public static void SetEulerAngle(this GameObject self, Vector3 eulerAngles) {
            self.transform.eulerAngles = eulerAngles;
        }

        /// <summary>
        /// X軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleX(this GameObject self, float x) {
            self.transform.SetEulerAngleX(x);
        }

        /// <summary>
        /// Y軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleY(this GameObject self, float y) {
            self.transform.SetEulerAngleY(y);
        }

        /// <summary>
        /// Z軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleZ(this GameObject self, float z) {
            self.transform.SetEulerAngleZ(z);
        }

        /// <summary>
        /// ローカル座標系の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngle(this GameObject self, Vector3 localEulerAngles) {
            self.transform.localEulerAngles = localEulerAngles;
        }

        /// <summary>
        /// ローカル座標系のX軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleX(this GameObject self, float x) {
            self.transform.SetLocalEulerAngleX(x);
        }

        /// <summary>
        /// ローカル座標系のY軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleY(this GameObject self, float y) {
            self.transform.SetLocalEulerAngleY(y);
        }

        /// <summary>
        /// ローカル座標系のZ軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleZ(this GameObject self, float z) {
            self.transform.SetLocalEulerAngleZ(z);
        }


        /// <summary>
        /// X軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleX(this GameObject self, float x) {
            self.transform.AddEulerAngleX(x);
        }

        /// <summary>
        /// Y軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleY(this GameObject self, float y) {
            self.transform.AddEulerAngleY(y);
        }

        /// <summary>
        /// Z軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleZ(this GameObject self, float z) {
            self.transform.AddEulerAngleZ(z);
        }

        /// <summary>
        /// ローカル座標系のX軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleX(this GameObject self, float x) {
            self.transform.AddLocalEulerAngleX(x);
        }

        /// <summary>
        /// ローカル座標系のY軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleY(this GameObject self, float y) {
            self.transform.AddLocalEulerAngleY(y);
        }

        /// <summary>
        /// ローカル座標系のX軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleZ(this GameObject self, float z) {
            self.transform.AddLocalEulerAngleZ(z);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region スケールの設定

        /// <summary>
        /// ローカル座標系の回転角を設定します
        /// </summary>
        public static void SetLocalScale(this GameObject self, Vector3 localScale) {
            self.transform.localScale = localScale;
        }

        /// <summary>
        /// X軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleX(this GameObject self, float x) {
            self.transform.SetLocalScaleX(x);
        }

        /// <summary>
        /// Y軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleY(this GameObject self, float y) {
            self.transform.SetLocalScaleY(y);
        }

        /// <summary>
        /// Z軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleZ(this GameObject self, float z) {
            self.transform.SetLocalScaleZ(z);
        }


        /// <summary>
        /// X軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleX(this GameObject self, float x) {
            self.transform.AddLocalScaleX(x);
        }

        /// <summary>
        /// Y軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleY(this GameObject self, float y) {
            self.transform.AddLocalScaleY(y);
        }

        /// <summary>
        /// Z軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleZ(this GameObject self, float z) {
            self.transform.AddLocalScaleZ(z);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 親子関係

        /// <summary>
        /// 親オブジェクトを設定する拡張メソッド
        /// </summary>
        public static GameObject SetParent(this GameObject self, Transform parent, bool worldPositionStays = true) {
            self.transform.SetParent(parent, worldPositionStays);
            return self;
        }

        /// <summary>
        /// 親オブジェクトを設定する拡張メソッド
        /// </summary>
        public static GameObject SetParent(this GameObject self, GameObject parent, bool worldPositionStays = true) {
            self.transform.SetParent(parent.transform, worldPositionStays);
            return self;
        }

        /// <summary>
        /// 親オブジェクトを設定する拡張メソッド
        /// </summary>
        public static void SetParentAndAlign(this GameObject self, GameObject parent) {
            if (parent == null) return;

            self.transform.SetParent(parent.transform, false);

            // 親と同じレイヤーと位置を与える (※GameObjectUtility.SetParentAndAlignと同じ)
            self.SetLayerRecursively(parent.layer);

            // 
            var rectTransform = self.transform as RectTransform;
            if (rectTransform) {
                rectTransform.anchoredPosition = Vector2.zero;
                Vector3 localPosition = rectTransform.localPosition;
                localPosition.z = 0;
                rectTransform.localPosition = localPosition;
            }
            // 
            else {
                self.transform.localPosition = Vector3.zero;
            }

            self.transform.localRotation = Quaternion.identity;
            self.transform.localScale = Vector3.one;
        }
        #endregion
    }

}