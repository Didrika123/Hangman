using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    public interface IHangman //RENAME THIS "GAME" unless you make an abstract hangman class providing some functionality which would otherwise be redudntantly impelmeneted in children
    {
        void init();
        void Update();
        void Draw();

        bool IsRunning();
    }
}
