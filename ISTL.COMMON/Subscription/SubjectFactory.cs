using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISTL.COMMON.Subscription
{
    public class SubjectFactory
    {
        private static SubjectFactory instance = null;
        private Dictionary<string, Subject> subjects;

        private SubjectFactory()
        {
            subjects = new Dictionary<string, Subject>();
        }

        public static SubjectFactory GetInstance() {
            if(instance == null) 
            {
                instance = new SubjectFactory();
            }
            return instance;
        }

        public void Add(string name, Subject sub)
        {
            subjects.Add(name, sub);
        }

        public void Remove(string name)
        {
            subjects.Remove(name);
        }

        public Subject GetSubject(string name)
        {
            return subjects[name];
        }
    }
}
