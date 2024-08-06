
namespace nitou {

    public interface IActivatable {

        /// <summary>
        /// アクティブな状態かどうか
        /// </summary>
        public bool IsActive { get; }

        public void Activate();
        public void Deactivate();
    }
}