using Gizeta.KCV.ManualCounter.Models;
using Grabacr07.KanColleWrapper;
using IronRuby;
using System.Reflection;

namespace Gizeta.KCV.ManualCounter.Utils
{
    public class RubyHost
    {
        private static readonly RubyHost instance = new RubyHost();

        private RubyHost()
        {
        }

        public static RubyHost Instace
        {
            get { return instance; }
        }

        public void Execute(Counter counter)
        {
            try
            {
                var engine = Ruby.CreateEngine();
                var scope = engine.CreateScope();
                scope.SetVariable("counter", counter);
                scope.SetVariable("kancolle_proxy", KanColleClient.Current.Proxy);
                engine.Runtime.LoadAssembly(Assembly.Load("System.Reactive.Core, Version=2.2.4.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"));
                engine.Runtime.LoadAssembly(Assembly.Load("System.Reactive.Linq, Version=2.2.4.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"));
                engine.Runtime.LoadAssembly(Assembly.Load("KanColleWrapper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"));
                engine.Execute(counter.Script, scope);
            }
            catch { }
        }
    }
}
