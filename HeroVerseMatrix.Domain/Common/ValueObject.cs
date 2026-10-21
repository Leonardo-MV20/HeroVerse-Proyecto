using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace HeroVerseMatrix.Domain.Common
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            var other = obj as ValueObject;
            if (other == null || other.GetType() != GetType())
            {
                return false;
            }

            using (var left = GetEqualityComponents().GetEnumerator())
            {
                using (var right = GetEqualityComponents().GetEnumerator())
                {
                    while (true)
                    {
                        var hasNextLeft = left.MoveNext();
                        var hasNextRight = right.MoveNext();

                        if (!hasNextLeft || !hasNextRight)
                        {
                            return hasNextLeft = hasNextRight;
                        }

                        if (!Equals(left.Current, right.Current))
                        {
                            return false;
                        }
                    }
                }
            }
        }
    }
}
