
using DAL.Domain;
using DAL.Repositories;

//using DAL.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICon_MerchantPortalAuthorizationRepository Con_MerchantPortalAuthorizationRepository { get; }
        ICon_PaymentAcceptProfileRepository Con_PaymentAcceptProfileRepository { get; }
        IApplicationLogRepository ApplicationLogRepository { get; }
        ICon_AccountApiAuthorizationRepository Con_AccountApiAuthorizationRepository { get; }
        ITran_CheckoutInfoRepository Tran_CheckoutInfoRepository { get; }
        ITran_TransactionInformationRepository Tran_TransactionInformationRepository { get; }
        IGen_BankInformationRepository Gen_BankInformationRepository { get; }
        IGen_AccountInformationRepository Gen_AccountInformationRepository { get; }
        IMerchantKYCRegistrationRepository MerchantKYCRegistrationRepository { get; }

        ICon_ChannelRepository Con_ChannelRepository { get; }
        ICon_ChannelTypeRepository Con_ChannelTypeRepository { get; }

        Task<int> CompleteAsync();
        void ResetContextState();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly E_PSOContext _context;
        public IMerchantKYCRegistrationRepository MerchantKYCRegistrationRepository { get; }
        public ICon_MerchantPortalAuthorizationRepository Con_MerchantPortalAuthorizationRepository { get; }
        public IGen_BankInformationRepository Gen_BankInformationRepository { get; }
        public ICon_PaymentAcceptProfileRepository Con_PaymentAcceptProfileRepository { get; }
        public IApplicationLogRepository ApplicationLogRepository { get; }
        public ICon_AccountApiAuthorizationRepository Con_AccountApiAuthorizationRepository { get; }
        public ITran_CheckoutInfoRepository Tran_CheckoutInfoRepository { get; }
        public ITran_TransactionInformationRepository Tran_TransactionInformationRepository { get; }
        public IGen_AccountInformationRepository Gen_AccountInformationRepository { get; }
        public ICon_ChannelRepository Con_ChannelRepository { get; }
        public ICon_ChannelTypeRepository Con_ChannelTypeRepository { get; }

        public UnitOfWork(

        IMerchantKYCRegistrationRepository MerchantKYCRegistrationRepository,
        ICon_MerchantPortalAuthorizationRepository Con_MerchantPortalAuthorizationRepository,
        IGen_BankInformationRepository Gen_BankInformationRepository,
        ICon_PaymentAcceptProfileRepository Con_PaymentAcceptProfileRepository,
        ITran_CheckoutInfoRepository Tran_CheckoutInfoRepository,
        ITran_TransactionInformationRepository Tran_TransactionInformationRepository,
        IGen_AccountInformationRepository Gen_AccountInformationRepository,
        ICon_ChannelRepository Con_ChannelRepository,
        ICon_ChannelTypeRepository Con_ChannelTypeRepository,
        ICon_AccountApiAuthorizationRepository Con_AccountApiAuthorizationRepository,
        IApplicationLogRepository ApplicationLogRepository,
            E_PSOContext context
           )
        {
            this.MerchantKYCRegistrationRepository = MerchantKYCRegistrationRepository;
            this.Con_MerchantPortalAuthorizationRepository = Con_MerchantPortalAuthorizationRepository;
            this.Gen_BankInformationRepository = Gen_BankInformationRepository;
            this.Con_PaymentAcceptProfileRepository = Con_PaymentAcceptProfileRepository;
            this.Con_ChannelRepository = Con_ChannelRepository;
            this.Con_ChannelTypeRepository = Con_ChannelTypeRepository;

            this.Tran_CheckoutInfoRepository = Tran_CheckoutInfoRepository;
            this.Tran_TransactionInformationRepository = Tran_TransactionInformationRepository;
            this.Gen_AccountInformationRepository = Gen_AccountInformationRepository;

            this.Con_AccountApiAuthorizationRepository = Con_AccountApiAuthorizationRepository;
            this.ApplicationLogRepository = ApplicationLogRepository;
            _context = context;
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void ResetContextState() => _context.ChangeTracker.Entries()
           .Where(e => e.Entity != null).ToList()
           .ForEach(e => e.State = EntityState.Detached);

        public async void Dispose()
        {
            await Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual async Task Dispose(bool disposing)
        {
            if (disposing)
            {
                await _context.DisposeAsync();
            }
        }

        // Begain Transaction

        public async void BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }
        //method to Save the changes permanently in the database

        public async void Commit()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async void Rollback()
        {
            await _context.Database.RollbackTransactionAsync();
            await _context.DisposeAsync();
        }
    }

}
