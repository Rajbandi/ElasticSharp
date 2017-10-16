using System;

namespace ElasticSharp.Core
{
    public static class Constants
    {
        public static readonly string AddressPrefix = "XEL-";

        public static readonly bool IsTestNet = false;

        public static readonly long[] FoundationMemberIds = { 3977330746712865438L, -1255995780042666937L, -7945280390087411431L, 8639008863476284945L };

        public static readonly int MaxNumberOfTransactions = 255;
        public static readonly int MinTransactionSize = 176;
        public static readonly int MaxPayloadLength = MaxNumberOfTransactions * MinTransactionSize;
        public static readonly ulong MaxBalanceNxt = 100000000;
        public static readonly ulong OneNxt = 100000000;
        public static readonly long TenthNxt = 10000000;
        public static readonly ulong MaxBalanceNqt = MaxBalanceNxt * OneNxt;
        public static readonly ulong InitialBaseTarget = 1537228670L;
        public static readonly ulong MaxBaseTarget = MaxBalanceNxt * InitialBaseTarget;
        public static readonly ulong MaxBaseTarget2 = IsTestNet ? MaxBaseTarget : InitialBaseTarget * 50;
        public static readonly ulong MinBaseTarget = InitialBaseTarget * 9 / 10;
        public static readonly int MinBlocktimeLimit = 53;
        public static readonly int MaxBlocktimeLimit = 67;
        public static readonly int BaseTargetGamma = 64;
        public static readonly ulong MinForgingBalanceNqt = 1000 * OneNxt;

        public static readonly int MaxTimedrift = 15; // allow up to 15 s clock difference
        public static readonly bool EnablePruning;

        public static readonly int MaxAccountNameLength = 100;
        public static readonly int MaxAccountDescriptionLength = 1000;

        public static readonly int MaxAccountPropertyNameLength = 32;
        public static readonly int MaxAccountPropertyValueLength = 160;

        public static readonly long MaxAssetQuantityQnt = 1000000000L * 100000000L;
        public static readonly int MinAssetNameLength = 3;
        public static readonly int MaxAssetNameLength = 10;
        public static readonly int MaxAssetDescriptionLength = 1000;
        public static readonly int MaxSingletonAssetDescriptionLength = 160;
        public static readonly int MaxAssetTransferCommentLength = 1000;
        public static readonly int MaxDividendPaymentRollback = 1441;

        public static readonly int MaxPollNameLength = 100;
        public static readonly int MaxPollDescriptionLength = 1000;
        public static readonly int MaxPollOptionLength = 100;
        public static readonly int MaxPollOptionCount = 100;
        public static readonly int MaxPollDuration = 14 * 1440;

        public static readonly byte MinVoteValue = unchecked((byte)-92);
        public static readonly byte MaxVoteValue = 92;
        public static readonly byte NoVoteValue = Byte.MinValue;

        public static readonly int MaxDgsListingQuantity = 1000000000;
        public static readonly int MaxDgsListingNameLength = 100;
        public static readonly int MaxDgsListingDescriptionLength = 1000;
        public static readonly int MaxDgsListingTagsLength = 100;
        public static readonly int MaxDgsGoodsLength = 1000;

        public static readonly int MaxHubAnnouncementUris = 100;
        public static readonly int MaxHubAnnouncementUriLength = 1000;
        public static readonly long MinHubEffectiveBalance = 100000;

        public static readonly int MinCurrencyNameLength = 3;
        public static readonly int MAX_CURRENCY_NAME_LENGTH = 10;
        public static readonly int MIN_CURRENCY_CODE_LENGTH = 3;
        public static readonly int MAX_CURRENCY_CODE_LENGTH = 5;
        public static readonly int MAX_CURRENCY_DESCRIPTION_LENGTH = 1000;
        public static readonly long MAX_CURRENCY_TOTAL_SUPPLY = 1000000000L * 100000000L;
        public static readonly int MAX_MINTING_RATIO = 10000; // per mint units not more than 0.01% of total supply
        public static readonly byte MIN_NUMBER_OF_SHUFFLING_PARTICIPANTS = 3;
        public static readonly byte MAX_NUMBER_OF_SHUFFLING_PARTICIPANTS = 30; // max possible at current block payload limit is 51
        public static readonly short MAX_SHUFFLING_REGISTRATION_PERIOD = (short)1440 * 7;
        

        public static readonly int MAX_TAGGED_DATA_NAME_LENGTH = 100;
        public static readonly int MAX_TAGGED_DATA_DESCRIPTION_LENGTH = 1000;
        public static readonly int MAX_TAGGED_DATA_TAGS_LENGTH = 100;
        public static readonly int MAX_TAGGED_DATA_TYPE_LENGTH = 100;
        public static readonly int MAX_TAGGED_DATA_CHANNEL_LENGTH = 100;
        public static readonly int MAX_TAGGED_DATA_FILENAME_LENGTH = 100;
        public static readonly int MAX_TAGGED_DATA_DATA_LENGTH = 42 * 1024;

        public static readonly int ALIAS_SYSTEM_BLOCK = -1;
        public static readonly int TRANSPARENT_FORGING_BLOCK = -1;
        public static readonly int ARBITRARY_MESSAGES_BLOCK = -1;
        public static readonly int TRANSPARENT_FORGING_BLOCK_2 = -1;
        public static readonly int TRANSPARENT_FORGING_BLOCK_3 = -1;
        public static readonly int TRANSPARENT_FORGING_BLOCK_4 = -1;
        public static readonly int TRANSPARENT_FORGING_BLOCK_5 = -1;
        public static readonly int TRANSPARENT_FORGING_BLOCK_6 = IsTestNet ? -1 : -1;
        public static readonly int TRANSPARENT_FORGING_BLOCK_7 = Int32.MaxValue;
        public static readonly int TRANSPARENT_FORGING_BLOCK_8 = IsTestNet ? -1 : -1;
        public static readonly int NQT_BLOCK = IsTestNet ? -1 : -1;
        public static readonly int FRACTIONAL_BLOCK = IsTestNet ? NQT_BLOCK : -1;
        public static readonly int ASSET_EXCHANGE_BLOCK = IsTestNet ? NQT_BLOCK : -1;
        public static readonly int ReferencedTransactionFullHashBlock = IsTestNet ? NQT_BLOCK : -1;
        public static readonly int ReferencedTransactionFullHashBlockTimestamp = -1;
        public static readonly int MaxReferencedTransactionTimespan = 60 * 1440 * 60;
        public static readonly int DigitalGoodsStoreBlock = -1;
        public static readonly int MonetarySystemBlock = -1;
        public static readonly int PhasingBlock = -1;
        public static readonly int ShufflingBlock = -1;
        public static readonly int FxtBlock = -1;



        public static readonly int ChecksumBlockGenesis = IsTestNet ? 0 : 0;

        public static readonly int LastChecksumBlock = ChecksumBlockGenesis;
        // LAST_KNOWN_BLOCK must also be set in html/www/js/nrs.constants.js
        public static readonly int LastKnownBlock = ChecksumBlockGenesis;

        public static readonly int[] MinVersion = new int[] { 3, 1, 0 };
        public static readonly int[] MinProxyVersion = new int[] { 3, 1, 0 };

        static readonly long UnconfirmedPoolDepositNqt = (IsTestNet ? 50 : 100) * TenthNxt;


        public static readonly byte TypePayment = 0;
        public static readonly byte TypeMessage = 1;
        public static readonly byte TypeAccountControl = 2;
        public static readonly byte TypeData = 3;

        public static readonly byte SubTypePaymentOrdinary = 0;
        public static readonly byte SubTypePaymentRedeem = 1;

        public static readonly byte SubTypeDataUpload = 0;
        public static readonly byte SubTypeDataExtend = 1;

        public static readonly byte SubTypeMessageArbitrary = 0;
        public static readonly byte SubTypeMessagePollCreation = 1;
        public static readonly byte SubTypeMessageVoteCasting = 2;
        public static readonly byte SubTypeMessageHubAnouncement = 3;
        public static readonly byte SubTypeMessageAccountInfo = 4;
        public static readonly byte SubTypeMessagePhasingVoteCasting = 5;

        public static readonly byte SubTypeAccountControlBalanceLeasing = 0;
        public static readonly byte SubTypeAccountControlPhasing = 1;

    }
}
