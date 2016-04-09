using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Scripts.UI.Screens
{
    public delegate void OnScreenModalResult(object result, object cookie);

    public class BaseScreen
    {
        protected Dictionary<string, int> transitionIndex = new Dictionary<string, int>();
        private Dictionary<string, GameObject> elements;
        private HashSet<string> duplicates;

        protected GameObject root;
        protected RectTransform recTransform;
        protected Animator screenAnimator;        

        public OnScreenModalResult OnModalResult { get; set; }
        public event OnScreenModalResult OnModalResultEvent;
        protected object modalResult;
        public object ModalResultCookie { get; set; }

        public BaseScreen( string asset )
        {
            root = UnityUtils.CreateGameObject(asset);
            recTransform = root.GetComponent<RectTransform>();
            screenAnimator = root.GetComponent<Animator>();

            elements = new Dictionary<string, GameObject>();
            duplicates = new HashSet<string>();

            OnLoaded();
        }

        public GameObject Root
        {
            get { return root; }
        }

        public Animator ScreenAnimator
        {
            get { return screenAnimator; }
        }

        public Transform transform
        {
            get { return root.transform; }
        }

        public RectTransform RectTransform
        {
            get { return recTransform; }
        }

        virtual public string ScreenName
        {
            get { return ""; }
        }

        virtual public string Layer
        {
            get { return Constants.Layers.WINDOW; }
        }

        virtual public void OnLoaded()
        {
            transitionIndex.Add("show", 0);
            transitionIndex.Add("hide", 1);

            if (screenAnimator != null )
            {
                screenAnimator.Play("show");
            }

            CreateElements(root);
        }

        virtual public void Close(object modalResult)
        {
            this.modalResult = modalResult;

            var screenManager = Service.Get<ScreenManager>();
            screenManager.RemoveScreen(this);            
        }

        private void CreateElements(GameObject parent)
        {
            AddElement(parent.name, parent);

            for (int i = 0, count = parent.transform.childCount; i < count; i++)
            {
                CreateElements(parent.transform.GetChild(i).gameObject);
            }
        }

        private void AddElement(string name, GameObject gob)
        {

            if (elements.ContainsKey(name))
            {
                // We can allow duplicate names, but only if no user code attempts to look it
                // up, since we wouldn't know which one they are asking for.  We used to error
                // right here, but now we are a little more lenient.  We will keep track of any
                // duplicates and error only if user code attempts to access them.
                if (!duplicates.Contains(name))
                {
                    duplicates.Add(name);
                }
            }
            else
            {
                elements.Add(name, gob);
            }
        }

        public GameObject GetElement(string name)
        {
            GameObject gob;
            if (elements.TryGetValue(name, out gob))
            {
                return gob;
            }

            Debug.LogError("Could not find " + typeof(GameObject) +  " named " + name + " in " + root == null ? "NULL" : root.name);
            return null;
        }
    }
}

