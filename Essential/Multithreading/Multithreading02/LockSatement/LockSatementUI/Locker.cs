using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace LockSatementUI
{
    public class Locker
    {
        private IView viewer;

        public Locker(IView viewer)
        {
            this.viewer = viewer;
        }

        public void LockZero() //argument is an expression of reference type
        {
            //int zero = 0;
            //lock (zero)
            //{

            //}
        }

        public void LockByGetType()
        {
            int zero = 0;
            lock (zero.GetType())
            {
                viewer.Show("Lock by GetType");

                Thread.Sleep(2000);
            }
        }

        public void LockByTypeOf()
        {
            lock (typeof(int))
            {
                viewer.Show("Lock by typeof keyword");

                Thread.Sleep(2000);
            }
        }

        public void LockByStringLiteral()
        {
            lock ("Hello")
            {
                viewer.Show("Lock by Hello");

                Thread.Sleep(2000);
            }
        }

        public void LockByUserClass(object obj)
        {
            A a = (A)obj;
            lock (a)
            {
                viewer.Show("Lock by user class");

                Thread.Sleep(2000);
            }
        }

        public void LockByObjectInstance(object obj)
        {
            lock (obj)
            {
                viewer.Show("Lock by object instance");

                Thread.Sleep(2000);
            }
        }

        public void LockByTypeOfObject()
        {
            lock (typeof(object))
            {
                viewer.Show("Lock by typeof object");

                Thread.Sleep(2000);
            }
        }

        public void LockByIntArray(object obj)
        {
            int[] ints = (int[])obj;

            lock (ints.SyncRoot)
            {
                viewer.Show("Lock by int array");

                RandomHalfSetupForArray(ints);

                Thread.Sleep(2000);
            }
        }

        private void RandomHalfSetupForArray(int[] ints)
        {
            Random random = new Random();

            int intsLengthHalf = ints.Length / 2;
            int startIndex = random.Next(ints.Length);
            for (int i = 0; i < intsLengthHalf; i++)
            {
                int index = (startIndex + i) % ints.Length;
                ints[index] = random.Next();
            }
        }

        public void LockByIntList(object obj)
        {
            List<int> ints = (List<int>)obj;

            ICollection collection = ints;
            lock (collection.SyncRoot)
            {
                viewer.Show("Lock by int list");

                const int addedCount = 5;
                RandomAddToList(ints, addedCount);

                const int removedCount = 3;
                RandomRemoveFromList(ints, removedCount);

                Thread.Sleep(2000);
            }
        }

        private void RandomAddToList(List<int> ints, int count)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
                ints.Add(random.Next());
        }

        private void RandomRemoveFromList(List<int> ints, int count)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
                ints.RemoveAt(random.Next(ints.Count));
        }

        public void LockThis()
        {
            lock (this)
            {
                viewer.Show("Lock by this keyword");

                Thread.Sleep(2000);
            }
        }
    }
}
