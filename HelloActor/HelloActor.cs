using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using HelloActor.Interfaces;

namespace HelloActor
{
    /// <remarks>
    /// このクラスはアクターを表します。
    /// 各 ActorID がこのクラスのインスタンスにマップされます。
    ///StatePersistence attribute 属性はアクターの状態の永続化とレプリケーションを次のように特定します:
    ///  - 永続化: 状態はディスクに書き込まれ、レプリケートされます。
    ///  - 可変: 状態はメモリに保持されるだけで、レプリケートされます。
    ///  - なし: 状態はメモリに保持されるだけで、レプリケートされません。
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class HelloActor : Actor, IHelloActor
    {
        /// <summary>
        /// このメソッドはアクターがアクティブになると必ず呼び出されます。
        /// アクターは、メソッドのいずれかが初めて呼び出されるときにアクティブ化されます。
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // StateManager は、このアクターのプライベート状態ストアです。
            // StateManager に格納されるデータは、揮発性の状態ストレージまたは永続化された状態ストレージを使用するアクターの高可用性を実現するためにレプリケートされます。
            // シリアル化されたオブジェクトは、すべて StateManager に保存できます。
            // 詳しくは、http://aka.ms/servicefabricactorsstateserialization をご覧ください

            return this.StateManager.TryAddStateAsync("count", 0);
        }

        /// <summary>
        /// TODO: 独自のアクター メソッドに置き換えます。
        /// </summary>
        /// <returns></returns>

        Task<int> IHelloActor.GetCountAsync()
        {
            return this.StateManager.GetStateAsync<int>("count");
        }

        /// <summary>
        /// TODO: 独自のアクター メソッドに置き換えます。
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task IHelloActor.SetCountAsync(int count)
        {
            // 要求が順序どおりに処理されることや、少なくとも一度処理されることは、保証されません。
            // 更新関数は、順序を保持するために、着信数が現在の数よりも大きいことを確認します。
            return this.StateManager.AddOrUpdateStateAsync("count", count, (key, value) => count > value ? count : value);
        }

        Task<string> IHelloActor.SayHelloAsync(string greeting)
        {
            return Task.FromResult("You said: '" + greeting + "', I say: Hello Nice to meet you!");
        }
    }
}
