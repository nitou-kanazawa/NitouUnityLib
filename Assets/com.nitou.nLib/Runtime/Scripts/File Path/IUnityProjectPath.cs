
namespace nitou{

    /// <summary>
    /// UnityProject内のデータ、またはディレクトリを指すパスのインターフェース
    /// </summary>
    public interface IUnityProjectPath{

        /// <summary>
        /// Projectディレクトリを起点としたパスに変換する
        /// </summary>
        string ToProjectPath();

        /// <summary>
        /// 絶対パスに変換する
        /// </summary>
        string ToAbsolutePath();
    }
}
