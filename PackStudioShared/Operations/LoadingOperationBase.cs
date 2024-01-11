﻿using PackStudio.Views;
using PackStudio.ViewModels;

namespace PackStudio.Operations
{
    public abstract class LoadingOperationBase
    {

        public LoadingOperation operation { get; private set; }
        public LoadingOperationViewModel ctx { get; private set; }
        public void Setup(LoadingOperation lo)
        {
            operation = lo;
            ctx = operation.DataContext as LoadingOperationViewModel;
        }
    }
}
