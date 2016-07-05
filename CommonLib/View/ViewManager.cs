using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.View
{
    /// <summary>
    /// The basic view manager
    /// </summary>
    public class ViewManager : IView
    {
        /// <summary>
        /// A lookup table for the views
        /// </summary>
        public Dictionary<string, IView> Views = new Dictionary<string, IView>();
        /// <summary>
        /// The application reference
        /// </summary>
        protected Application App;
        /// <summary>
        /// Called every update cycle; updates all views
        /// </summary>
        /// <param name="deltaTime">the time since the program started</param>
        public void Update(TimeSpan deltaTime)
        {
            foreach(var view in Views)
            {
                view.Value.Update(deltaTime);
            }
        }
        /// <summary>
        /// Initializes the views
        /// </summary>
        /// <param name="app">application to init the view manager and views with</param>
        public void Init(Application app)
        {
            App = app;
            foreach (var view in Views)
            {
                view.Value.Init(app);
            }
        }
        /// <summary>
        /// Cleanup any unamanged or other resources
        /// </summary>
        public void Destroy()
        {
            foreach(var view in Views)
            {
                view.Value.Destroy();
            }
        }
    }
}
