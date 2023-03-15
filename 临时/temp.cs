class Program
    {
        static void Main(string[] args)
        {
            Customer c = new Customer();
            EventInfo evt = c.GetType().GetEvent("OnChange",
                    BindingFlags.NonPublic | BindingFlags.Instance
                    | BindingFlags.Public
                );
            // 添加一个事件关联
            evt.AddEventHandler(c, new EventHandler(c_OnChange));
            // 添加第二个事件关联
            evt.AddEventHandler(c, new EventHandler(c_OnChange));

            // 删除全部事件关联。
            RemoveEvent<Customer>(c, "OnChange");

            c.Change();
        }

        static void c_OnChange(object sender, EventArgs e)
        {
            Console.WriteLine("事件被触发了");
        }

        static void RemoveEvent<T>(T c, string name)
        {
            Delegate[] invokeList = GetObjectEventList(c, name);
            if (invokeList == null)
                return;
            foreach (Delegate del in invokeList)
            {
                typeof(T).GetEvent(name).RemoveEventHandler(c, del);
            }
        }

        public static Delegate[] GetObjectEventList(object p_Object, string p_EventName)
        {
            // Get event field
            FieldInfo _Field = p_Object.GetType().GetField(p_EventName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            if (_Field == null)
            {
                return null;
            }
            // get the value of event field which should be a delegate
            object _FieldValue = _Field.GetValue(p_Object);

            // if it is a delegate
            if (_FieldValue != null && _FieldValue is Delegate)
            {
                // cast the value to a delegate
                Delegate _ObjectDelegate = (Delegate)_FieldValue;
                // get the invocation list
                return _ObjectDelegate.GetInvocationList();
            }
            return null;
        } 
    }

    class Customer
    {
        public event EventHandler OnChange;
        public void Change()
        {
            if (OnChange != null)
                OnChange(this, null);
            else
                Console.WriteLine("no event attached");
        }
    }