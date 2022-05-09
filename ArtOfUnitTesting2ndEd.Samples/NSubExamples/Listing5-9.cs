using System;
using Chapter5.LogAn;
using NSubstitute;
using NUnit.Framework;

namespace NSubExamples
{
    class Presenter
    {
        private readonly IView _view;
        private readonly ILogger _log;

        public Presenter(IView view,ILogger log)
        {
            _view = view;
            _log = log;
            this._view.Loaded += OnLoaded;
            this._view.ErrorOccured += OnError;

        }

        private void OnError(string text)
        {
            _log.LogError(text);
        }

        private void OnLoaded()
        {
            _view.Render("Hello World");
        }
    }

    public interface IView
    {
        event Action Loaded;
        event Action<string> ErrorOccured;
        void Render(string text);
    }

    [TestFixture]
    class EventRelatedTests
    {
        [Test]
        public void ctor_WhenViewIsLoaded_CallsViewRender()
        {
            var mockView = Substitute.For<IView>();

            var stubLogger = Substitute.For<ILogger>();
            Presenter p = new Presenter(mockView, stubLogger);
            mockView.Loaded += Raise.Event<Action>();//引發某個事件時(不帶參數)

            mockView.Received().Render(Arg.Is<string>(s => s.Contains("Hello World")));
        }

        [Test]
        public void ctor_WhenViewhasError_CallsLogger()
        {
            var stubView = Substitute.For<IView>();
            var mockLogger = Substitute.For<ILogger>();

            Presenter p = new Presenter(stubView, mockLogger);
            //引發某個事件帶參數
            stubView.ErrorOccured += Raise.Event<Action<string>>("fake error");
        
            mockLogger.Received().LogError(Arg.Is<string>(s => s.Contains("fake error")));
        }
 
    }

    }





