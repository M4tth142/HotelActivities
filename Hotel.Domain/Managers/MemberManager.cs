using Hotel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Domain.Model;
using Hotel.Domain.Exceptions;

namespace Hotel.Domain.Managers
{
    public class MemberManager
    {
        IMemberRepository _memberRepository;
        public MemberManager(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public IReadOnlyList<Member> GetMember(string filter)
        {
            try
            {
                return _memberRepository.GetMember(filter);
            }
            catch (Exception ex)
            {
                throw new MemberManagerException("GetMembers", ex);
            }
        }
    }
}
