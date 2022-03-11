using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafetyChainChallenge
{
    public interface IDirectoryService
    {
        void GetDirectory(string path, ref StringBuilder csvFile);
    }
}
