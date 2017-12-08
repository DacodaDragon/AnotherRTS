using AnotherRTS.Util;

namespace AnotherRTS.Management.Input
{
    public class ControlScheme
    {
        ControlGroup[] m_active;
        ControlGroup[] m_inactive;

        public void AddControllGroup(bool active, params ControlGroup[] groups)
        {
            if (active)
            {
                ArrayUtil.AddToArray(m_active, groups);
            }
            else ArrayUtil.AddToArray(m_inactive, groups);
        }

        private ControlGroup CheckControlGroup(ControlGroup[] arrays, string Name)
        {
            for (int i = 0; i < arrays.Length; i++)
            {
                if (arrays[i].Name == Name)
                {
                    return arrays[i];
                }
            }
            return null;
        }

        private ControlGroup FindGroup(string name)
        {
            ControlGroup context;

            context = CheckControlGroup(m_active, name);
            if (context == null)
                context = CheckControlGroup(m_inactive, name);
            return context;
        }

        public void Activate(string name)
        {
            ControlGroup context = CheckControlGroup(m_active, name);
            ArrayUtil.RemoveFromArray(m_active, context);
            ArrayUtil.AddToArray(m_inactive, context);
        }

        public void Deactivate(string name)
        {
            ControlGroup context = CheckControlGroup(m_inactive, name);
            ArrayUtil.RemoveFromArray(m_inactive, context);
            ArrayUtil.AddToArray(m_active, context);
        }

        public void ClearAll()
        {
            m_inactive = null;
            m_active = null;
        }

        public bool Hook(string name, KeyDelegate handle)
        {
            ControlGroup context = FindGroup(name);

            // fail if this container doesn't
            // contain the right keycode
            if (context == null)
                return false;

            return context.Hook(name, handle);
        }

        public bool UnHook(string name, KeyDelegate handle)
        {
            ControlGroup context = FindGroup(name);

            // fail if this container doesn't
            // contain the right keycode
            if (context == null)
                return false;

            return context.UnHook(name, handle);
        }
    }
}