using System;

namespace GeletaApp
{
    public interface IBackgroundDependency
    {
        void ExecuteCommand(string a, string b, string c, Action d);
    }
}