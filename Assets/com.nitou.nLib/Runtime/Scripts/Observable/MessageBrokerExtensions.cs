using System;

// [REF]
//  PG日誌: MessageBrokerを拡張する(Pub/Subの型指定) https://takap-tech.com/entry/2023/01/23/234127

namespace UniRx {

    // [NOTE] 
    //  デフォルトの IMessagePublisher/IMessageReceiver は型指定ができない．（→どんな型でも扱えてしまう）
    //  よって型指定が可能な Pub/Sub を追加する．


    /// <summary>
    /// 明示的に型を指定したPublisher．
    /// </summary>
    public interface IMessagePublisher<T> {

        /// <summary>
        /// Send Message to all receiver.
        /// </summary>
        void Publish(T message);
    }

    /// <summary>
    /// 明示的に型を指定したReceiver．
    /// </summary>
    public interface IMessageReceiver<T> {

        /// <summary>
        /// Subscribe typed message.
        /// </summary>
        IObservable<T> Receive();
    }

    public interface IMessageBroker<T> : IMessagePublisher<T>, IMessageReceiver<T> {

        public class DefaultImpl : IMessageBroker<T> {
            private readonly IMessageBroker _service;
            public DefaultImpl(IMessageBroker service) => _service = service;
            public void Publish(T message) => _service.Publish(message);
            public IObservable<T> Receive() => _service.Receive<T>();
        }
    }


    public static partial class MessageBrokerExtensions {

        public static IMessagePublisher<T> GetPublisher<T>(this IMessageBroker self) {
            return new IMessageBroker<T>.DefaultImpl(self);
        }

        public static IMessageReceiver<T> GetSubscriber<T>(this IMessageBroker self) {
            return new IMessageBroker<T>.DefaultImpl(self);
        }
    }

    // 直接Subscribeできるようにメソッドを追加する
    public static partial class IMessageReceiverExtensions {

        /// <summary>
        /// 直接 Subscribe する拡張メソッド．
        /// </summary>
        public static IDisposable Subscribe<T>(this IMessageReceiver self, Action<T> action) {
            return self.Receive<T>().Subscribe(action);
        }

        /// <summary>
        /// 直接 Subscribe する拡張メソッド．
        /// </summary>
        public static IDisposable Subscribe<T>(this IMessageReceiver<T> self, Action<T> action) {
            return self.Receive().Subscribe(action);
        }
    }
}
