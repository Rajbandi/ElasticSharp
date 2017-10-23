/**********************************************

 Copyright (c) 2017 Raj Bandi
 Licensed under Apache License 2.0. See LICENSE file in the project root for full license information.
 
 Name: Constants.cs 
 Project: ElasticSharp (https://www.github.com/rajbandi/elasticsharp)
   
***********************************************/
using System;

namespace ElasticSharp.Core
{
    /// <summary>
    /// All Elastic constants
    /// </summary>
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

        public static int MaxArbitraryMessageLength = 160;
        public static int MaxEncryptedMessageLength = 160 + 16;

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
        public static readonly int MaxCurrencyNameLength = 10;
        public static readonly int MinCurrencyCodeLength = 3;
        public static readonly int MaxCurrencyCodeLength = 5;
        public static readonly int MaxCurrencyDescriptionLength = 1000;
        public static readonly long MaxCurrencyTotalSupply = 1000000000L * 100000000L;
        public static readonly int MaxMintingRatio = 10000; // per mint units not more than 0.01% of total supply
        public static readonly byte MinNumberOfShufflingParticipants = 3;
        public static readonly byte MaxNumberOfShufflingParticipants = 30; // max possible at current block payload limit is 51
        public static readonly short MaxShufflingRegistrationPeriod = (short)1440 * 7;
        

        public static readonly int MaxTaggedDataNameLength = 100;
        public static readonly int MaxTaggedDataDescriptionLength = 1000;
        public static readonly int MaxTaggedDataTagsLength = 100;
        public static readonly int MaxTaggedDataTypeLength = 100;
        public static readonly int MaxTaggedDataChannelLength = 100;
        public static readonly int MaxTaggedDataFilenameLength = 100;
        public static readonly int MaxTaggedDataDataLength = 42 * 1024;

        public static readonly int AliasSystemBlock = -1;
        public static readonly int TransparentForgingBlock = -1;
        public static readonly int ArbitraryMessagesBlock = -1;
        public static readonly int TransparentForgingBlock2 = -1;
        public static readonly int TransparentForgingBlock3 = -1;
        public static readonly int TransparentForgingBlock4 = -1;
        public static readonly int TransparentForgingBlock5 = -1;
        public static readonly int TransparentForgingBlock6 = IsTestNet ? -1 : -1;
        public static readonly int TransparentForgingBlock7 = Int32.MaxValue;
        public static readonly int TransparentForgingBlock8 = IsTestNet ? -1 : -1;
        public static readonly int NqtBlock = IsTestNet ? -1 : -1;
        public static readonly int FractionalBlock = IsTestNet ? NqtBlock : -1;
        public static readonly int AssetExchangeBlock = IsTestNet ? NqtBlock : -1;
        public static readonly int ReferencedTransactionFullHashBlock = IsTestNet ? NqtBlock : -1;
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
