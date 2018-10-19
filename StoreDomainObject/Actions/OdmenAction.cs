using StoreDomainObject.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject
{
    public class OdmenAction
    {
        public void CreateGroup(GoodGroup group)
        {
            var odmen = new Odmen();
            odmen.CreateGroup(group);
        }
        public void DeleteGroup(long groupId)
        {
            var odmen = new Odmen();
            odmen.DeleteGroup(groupId);
        }
        public void ChangeGroup(GoodGroup group)
        {
            var odmen = new Odmen();
            odmen.ChangeGroup(group);
        }

        public void CreateGood(Good good)
        {
            var odmen = new Odmen();
            odmen.CreateGood(good);
        }
        public void DeleteGood(long goodId)
        {
            var odmen = new Odmen();
            odmen.DeleteGood(goodId);
        }
        public void ChangeGood(Good good)
        {
            var odmen = new Odmen();
            odmen.ChangeGood(good);
        }

    }
}
