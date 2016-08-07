﻿using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors.Runtime;

namespace HelloActor
{
    internal static class Program
    {
        /// <summary>
        /// これは、サービス ホスト プロセスのエントリ ポイントです。
        /// </summary>
        private static void Main()
        {
            try
            {
                // この行は、Service Fabric ランタイムにアクター クラスをホストするアクター サービスを登録します。
                // ServiceManifest.xml ファイルと ApplicationManifest.xml ファイルのコンテンツ
                // は、このプロジェクトをビルドするときに自動的に設定されます。
                // 詳しくは、http://aka.ms/servicefabricactorsplatform をご覧ください

                ActorRuntime.RegisterActorAsync<HelloActor>(
                   (context, actorType) => new ActorService(context, actorType, () => new HelloActor())).GetAwaiter().GetResult();

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ActorEventSource.Current.ActorHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
