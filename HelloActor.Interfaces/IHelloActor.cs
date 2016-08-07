using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace HelloActor.Interfaces
{
    /// <summary>
    /// このインターフェイスは、アクターが公開するメソッドを定義します。
    /// クライアントはこのインターフェイスを使用して、インターフェイスを実装するアクターと対話します。
    /// </summary>
    public interface IHelloActor : IActor
    {
        /// <summary>
        /// TODO: 独自のアクター メソッドに置き換えます。
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// TODO: 独自のアクター メソッドに置き換えます。
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task SetCountAsync(int count);

        Task<string> SayHelloAsync(string greeting);

    }
}
