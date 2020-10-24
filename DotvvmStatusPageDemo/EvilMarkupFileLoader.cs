using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;

namespace DotvvmStatusPageDemo
{
    public class EvilMarkupFileLoader : IMarkupFileLoader
    {
        private DefaultMarkupFileLoader _defaultMarkupFileLoader;

        public EvilMarkupFileLoader()
        {
            _defaultMarkupFileLoader=new DefaultMarkupFileLoader();
        }
        
        public MarkupFile? GetMarkup(DotvvmConfiguration configuration, string virtualPath)
        {
            var markupFile = _defaultMarkupFileLoader.GetMarkup(configuration, virtualPath);

            if (virtualPath.Contains("Ugly"))
            {
                EvilizeContentReaderFunc(markupFile, () =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                });
            }

            return markupFile;

        }

        public string GetMarkupFileVirtualPath(IDotvvmRequestContext context)
        {
            return _defaultMarkupFileLoader.GetMarkupFileVirtualPath(context);
        }



        private static void EvilizeContentReaderFunc(MarkupFile markupFile, Action evilAction)
        {

            var evilFunc = new System.Func<string>(() =>
            {
                evilAction();
                return new MarkupFile(markupFile.FileName,markupFile.FullPath).ContentsReaderFactory();
            });
            var contentReaderFactoryPropertyInfo = typeof(MarkupFile).GetProperty(nameof(markupFile.ContentsReaderFactory));
            contentReaderFactoryPropertyInfo.SetValue(markupFile, evilFunc);
        }
    }

}
