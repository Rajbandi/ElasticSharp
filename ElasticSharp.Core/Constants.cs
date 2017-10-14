using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSharp.Core
{
    public static class Constants
    {
        public static readonly bool IsTestNet = false;

        public static readonly long[] FOUNDATION_MEMBER_IDS = { 3977330746712865438L, -1255995780042666937L, -7945280390087411431L, 8639008863476284945L };

        public static readonly int MAX_NUMBER_OF_TRANSACTIONS = 255;
        public static readonly int MIN_TRANSACTION_SIZE = 176;
        public static readonly int MAX_PAYLOAD_LENGTH = MAX_NUMBER_OF_TRANSACTIONS * MIN_TRANSACTION_SIZE;
        public static readonly ulong MAX_BALANCE_NXT = 100000000;
        public static readonly ulong ONE_NXT = 100000000;
        public static readonly long TENTH_NXT = 10000000;
        public static readonly ulong MAX_BALANCE_NQT = MAX_BALANCE_NXT * ONE_NXT;
        public static readonly ulong INITIAL_BASE_TARGET = 1537228670L;
        public static readonly ulong MAX_BASE_TARGET = MAX_BALANCE_NXT * INITIAL_BASE_TARGET;
        public static readonly ulong MAX_BASE_TARGET_2 = IsTestNet ? MAX_BASE_TARGET : INITIAL_BASE_TARGET * 50;
        public static readonly ulong MIN_BASE_TARGET = INITIAL_BASE_TARGET * 9 / 10;
        public static readonly int MIN_BLOCKTIME_LIMIT = 53;
        public static readonly int MAX_BLOCKTIME_LIMIT = 67;
        public static readonly int BASE_TARGET_GAMMA = 64;
        public static readonly ulong MIN_FORGING_BALANCE_NQT = 1000 * ONE_NXT;

        public static readonly int MAX_TIMEDRIFT = 15; // allow up to 15 s clock difference
        public static readonly bool ENABLE_PRUNING;

        public static readonly int MAX_ACCOUNT_NAME_LENGTH = 100;
        public static readonly int MAX_ACCOUNT_DESCRIPTION_LENGTH = 1000;

        public static readonly int MAX_ACCOUNT_PROPERTY_NAME_LENGTH = 32;
        public static readonly int MAX_ACCOUNT_PROPERTY_VALUE_LENGTH = 160;

        public static readonly long MAX_ASSET_QUANTITY_QNT = 1000000000L * 100000000L;
        public static readonly int MIN_ASSET_NAME_LENGTH = 3;
        public static readonly int MAX_ASSET_NAME_LENGTH = 10;
        public static readonly int MAX_ASSET_DESCRIPTION_LENGTH = 1000;
        public static readonly int MAX_SINGLETON_ASSET_DESCRIPTION_LENGTH = 160;
        public static readonly int MAX_ASSET_TRANSFER_COMMENT_LENGTH = 1000;
        public static readonly int MAX_DIVIDEND_PAYMENT_ROLLBACK = 1441;

        public static readonly int MAX_POLL_NAME_LENGTH = 100;
        public static readonly int MAX_POLL_DESCRIPTION_LENGTH = 1000;
        public static readonly int MAX_POLL_OPTION_LENGTH = 100;
        public static readonly int MAX_POLL_OPTION_COUNT = 100;
        public static readonly int MAX_POLL_DURATION = 14 * 1440;

        public static readonly byte MIN_VOTE_VALUE = unchecked((byte)-92);
        public static readonly byte MAX_VOTE_VALUE = 92;
        public static readonly byte NO_VOTE_VALUE = Byte.MinValue;

        public static readonly int MAX_DGS_LISTING_QUANTITY = 1000000000;
        public static readonly int MAX_DGS_LISTING_NAME_LENGTH = 100;
        public static readonly int MAX_DGS_LISTING_DESCRIPTION_LENGTH = 1000;
        public static readonly int MAX_DGS_LISTING_TAGS_LENGTH = 100;
        public static readonly int MAX_DGS_GOODS_LENGTH = 1000;

        public static readonly int MAX_HUB_ANNOUNCEMENT_URIS = 100;
        public static readonly int MAX_HUB_ANNOUNCEMENT_URI_LENGTH = 1000;
        public static readonly long MIN_HUB_EFFECTIVE_BALANCE = 100000;

        public static readonly int MIN_CURRENCY_NAME_LENGTH = 3;
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
        public static readonly int REFERENCED_TRANSACTION_FULL_HASH_BLOCK = IsTestNet ? NQT_BLOCK : -1;
        public static readonly int REFERENCED_TRANSACTION_FULL_HASH_BLOCK_TIMESTAMP = IsTestNet ? -1 : -1;
        public static readonly int MAX_REFERENCED_TRANSACTION_TIMESPAN = 60 * 1440 * 60;
        public static readonly int DIGITAL_GOODS_STORE_BLOCK = IsTestNet ? -1 : -1;
        public static readonly int MONETARY_SYSTEM_BLOCK = IsTestNet ? -1 : -1;
        public static readonly int PHASING_BLOCK = IsTestNet ? -1 : -1;
        public static readonly int SHUFFLING_BLOCK = IsTestNet ? -1 : -1;
        public static readonly int FXT_BLOCK = IsTestNet ? -1 : -1;



        public static readonly int CHECKSUM_BLOCK_GENESIS = IsTestNet ? 0 : 0;

        public static readonly int LAST_CHECKSUM_BLOCK = CHECKSUM_BLOCK_GENESIS;
        // LAST_KNOWN_BLOCK must also be set in html/www/js/nrs.constants.js
        public static readonly int LAST_KNOWN_BLOCK = CHECKSUM_BLOCK_GENESIS;

        public static readonly int[] MIN_VERSION = new int[] { 3, 1, 0 };
        public static readonly int[] MIN_PROXY_VERSION = new int[] { 3, 1, 0 };

        static readonly long UNCONFIRMED_POOL_DEPOSIT_NQT = (IsTestNet ? 50 : 100) * TENTH_NXT;
    }
}
