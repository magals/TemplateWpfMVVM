using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateWpfMVVM.Infostructure.Commands.Base;

namespace TemplateWpfMVVM.Infostructure.Commands
{
    internal class LamdaCommand : Command
    {
        public LamdaCommand(Action<object> Execute, Func<object, bool> CanExcute = null)
        {
            _Execute = Execute ?? throw new ArgumentException(nameof(Execute));
            _CanExcute = CanExcute;
        }

        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExcute;

        public override bool CanExecute(object parameter) => _CanExcute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter)=>_Execute(parameter);
    }
}
