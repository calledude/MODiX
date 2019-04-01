﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Audio;
using Discord.Rest;

namespace Discord.WebSocket
{
    /// <inheritdoc cref="SocketGuild" />
    public interface ISocketGuild : IGuild
    {
        /// <inheritdoc cref="SocketGuild.Roles" />
        new IReadOnlyCollection<ISocketRole> Roles { get; }

        /// <inheritdoc cref="SocketGuild.Owner" />
        ISocketGuildUser Owner { get; }

        /// <inheritdoc cref="SocketGuild.MemberCount" />
        int MemberCount { get; }

        /// <inheritdoc cref="SocketGuild.Users" />
        IReadOnlyCollection<ISocketGuildUser> Users { get; }

        /// <inheritdoc cref="SocketGuild.IsConnected" />
        bool IsConnected { get; }

        /// <inheritdoc cref="SocketGuild.IsSynced" />
        bool IsSynced { get; }

        /// <inheritdoc cref="SocketGuild.HasAllMembers" />
        bool HasAllMembers { get; }

        /// <inheritdoc cref="SocketGuild.SyncPromise" />
        Task SyncPromise { get; }

        /// <inheritdoc cref="SocketGuild.Emotes" />
        new IReadOnlyCollection<IGuildEmote> Emotes { get; }

        /// <inheritdoc cref="SocketGuild.Channels" />
        IReadOnlyCollection<ISocketGuildChannel> Channels { get; }

        /// <inheritdoc cref="SocketGuild.EveryoneRole" />
        new ISocketRole EveryoneRole { get; }

        /// <inheritdoc cref="SocketGuild.CurrentUser" />
        ISocketGuildUser CurrentUser { get; }

        /// <inheritdoc cref="SocketGuild.CategoryChannels" />
        IReadOnlyCollection<ISocketCategoryChannel> CategoryChannels { get; }

        /// <inheritdoc cref="SocketGuild.VoiceChannels" />
        IReadOnlyCollection<ISocketVoiceChannel> VoiceChannels { get; }

        /// <inheritdoc cref="SocketGuild.TextChannels" />
        IReadOnlyCollection<ISocketTextChannel> TextChannels { get; }

        /// <inheritdoc cref="SocketGuild.SystemChannel" />
        ISocketTextChannel SystemChannel { get; }

        /// <inheritdoc cref="SocketGuild.DownloadedMemberCount" />
        int DownloadedMemberCount { get; }

        /// <inheritdoc cref="SocketGuild.AFKChannel" />
        ISocketVoiceChannel AFKChannel { get; }

        /// <inheritdoc cref="SocketGuild.DefaultChannel" />
        ISocketTextChannel DefaultChannel { get; }

        /// <inheritdoc cref="SocketGuild.DownloaderPromise" />
        Task DownloaderPromise { get; }

        /// <inheritdoc cref="SocketGuild.EmbedChannel" />
        ISocketGuildChannel EmbedChannel { get; }

        /// <inheritdoc cref="SocketGuild.AddGuildUserAsync(ulong, string, Action{AddGuildUserProperties}, RequestOptions)" />
        new Task<IRestGuildUser> AddGuildUserAsync(ulong id, string accessToken, Action<AddGuildUserProperties> func = null, RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.CreateCategoryChannelAsync(string, RequestOptions)" />
        Task<IRestCategoryChannel> CreateCategoryChannelAsync(string name, RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.CreateEmoteAsync(string, Image, Optional{IEnumerable{IRole}}, RequestOptions)" />
        new Task<IGuildEmote> CreateEmoteAsync(string name, Image image, Optional<IEnumerable<IRole>> roles = default, RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.CreateIntegrationAsync(ulong, string, RequestOptions)" />
        new Task<IRestGuildIntegration> CreateIntegrationAsync(ulong id, string type, RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.CreateRoleAsync(string, GuildPermissions?, Color?, bool, RequestOptions)" />
        new Task<IRestRole> CreateRoleAsync(string name, GuildPermissions? permissions = null, Color? color = null, bool isHoisted = false, RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.CreateTextChannelAsync(string, Action{TextChannelProperties}, RequestOptions)" />
        new Task<IRestTextChannel> CreateTextChannelAsync(string name, Action<TextChannelProperties> func = null, RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.CreateVoiceChannelAsync(string, Action{VoiceChannelProperties}, RequestOptions)" />
        new Task<IRestVoiceChannel> CreateVoiceChannelAsync(string name, Action<VoiceChannelProperties> func = null, RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.GetAuditLogsAsync(int, RequestOptions)" />
        IAsyncEnumerable<IReadOnlyCollection<IRestAuditLogEntry>> GetAuditLogsAsync(int limit, RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.GetBanAsync(ulong, RequestOptions)" />
        new Task<IBan> GetBanAsync(ulong userId, RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.GetBanAsync(IUser, RequestOptions)" />
        new Task<IBan> GetBanAsync(IUser user, RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.GetBansAsync(RequestOptions)" />
        new Task<IReadOnlyCollection<IBan>> GetBansAsync(RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.GetChannel(ulong)" />
        ISocketGuildChannel GetChannel(ulong id);

        /// <inheritdoc cref="SocketGuild.GetEmoteAsync(ulong, RequestOptions)" />
        new Task<IGuildEmote> GetEmoteAsync(ulong id, RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.GetIntegrationsAsync(RequestOptions)" />
        new Task<IReadOnlyCollection<IRestGuildIntegration>> GetIntegrationsAsync(RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.GetInvitesAsync(RequestOptions)" />
        new Task<IReadOnlyCollection<IRestInviteMetadata>> GetInvitesAsync(RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.GetRole(ulong)" />
        new ISocketRole GetRole(ulong id);

        /// <inheritdoc cref="SocketGuild.GetTextChannel(ulong)" />
        ISocketTextChannel GetTextChannel(ulong id);

        /// <inheritdoc cref="SocketGuild.GetUser(ulong)" />
        ISocketGuildUser GetUser(ulong id);

        /// <inheritdoc cref="SocketGuild.GetVanityInviteAsync(RequestOptions)" />
        new Task<IRestInviteMetadata> GetVanityInviteAsync(RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.GetVoiceChannel(ulong)" />
        ISocketVoiceChannel GetVoiceChannel(ulong id);

        /// <inheritdoc cref="SocketGuild.GetVoiceRegionsAsync(RequestOptions)" />
        new Task<IReadOnlyCollection<IRestVoiceRegion>> GetVoiceRegionsAsync(RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.GetWebhookAsync(ulong, RequestOptions)" />
        new Task<IRestWebhook> GetWebhookAsync(ulong id, RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.GetWebhooksAsync(RequestOptions)" />
        new Task<IReadOnlyCollection<IRestWebhook>> GetWebhooksAsync(RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.ModifyEmoteAsync(GuildEmote, Action{EmoteProperties}, RequestOptions)" />
        Task<IGuildEmote> ModifyEmoteAsync(IGuildEmote emote, Action<EmoteProperties> func, RequestOptions options = null);

        /// <inheritdoc cref="SocketGuild.DeleteEmoteAsync(GuildEmote, RequestOptions)" />
        Task DeleteEmoteAsync(IGuildEmote emote, RequestOptions options = null);
    }

    /// <summary>
    /// Provides an abstraction wrapper layer around a <see cref="WebSocket.SocketGuild"/>, through the <see cref="ISocketGuild"/> interface.
    /// </summary>
    public class SocketGuildAbstraction : ISocketGuild
    {
        /// <summary>
        /// Constructs a new <see cref="SocketGuildAbstraction"/> around an existing <see cref="WebSocket.SocketGuild"/>.
        /// </summary>
        /// <param name="socketGuild">The value to use for <see cref="WebSocket.SocketGuild"/>.</param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="socketGuild"/>.</exception>
        public SocketGuildAbstraction(SocketGuild socketGuild)
        {
            SocketGuild = socketGuild ?? throw new ArgumentNullException(nameof(socketGuild));
        }

        /// <inheritdoc />
        public ISocketVoiceChannel AFKChannel
            => SocketGuild.AFKChannel
                .Abstract();

        /// <inheritdoc />
        public ulong? AFKChannelId
            => (SocketGuild as IGuild).AFKChannelId;

        /// <inheritdoc />
        public int AFKTimeout
            => SocketGuild.AFKTimeout;

        /// <inheritdoc />
        public ulong? ApplicationId
            => SocketGuild.ApplicationId;

        /// <inheritdoc />
        public IAudioClient AudioClient
            => SocketGuild.AudioClient;

        /// <inheritdoc />
        public bool Available
            => (SocketGuild as IGuild).Available;

        /// <inheritdoc />
        public IReadOnlyCollection<ISocketCategoryChannel> CategoryChannels
            => SocketGuild.CategoryChannels
                .Select(SocketCategoryChannelAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        public IReadOnlyCollection<ISocketGuildChannel> Channels
            => SocketGuild.Channels
                .Select(SocketGuildChannelAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        public DateTimeOffset CreatedAt
            => SocketGuild.CreatedAt;

        /// <inheritdoc />
        public ISocketGuildUser CurrentUser
            => SocketGuild.CurrentUser
                .Abstract();

        /// <inheritdoc />
        public ISocketTextChannel DefaultChannel
            => SocketGuild.DefaultChannel
                .Abstract();

        /// <inheritdoc />
        public ulong DefaultChannelId
            => (SocketGuild as IGuild).DefaultChannelId;

        /// <inheritdoc />
        public DefaultMessageNotifications DefaultMessageNotifications
            => SocketGuild.DefaultMessageNotifications;

        /// <inheritdoc />
        public int DownloadedMemberCount
            => SocketGuild.DownloadedMemberCount;

        /// <inheritdoc />
        public Task DownloaderPromise
            => SocketGuild.DownloaderPromise;

        /// <inheritdoc />
        public ISocketGuildChannel EmbedChannel
            => SocketGuild.EmbedChannel
                .Abstract();

        /// <inheritdoc />
        public ulong? EmbedChannelId
            => (SocketGuild as IGuild).EmbedChannelId;

        /// <inheritdoc />
        public IReadOnlyCollection<IGuildEmote> Emotes
            => SocketGuild.Emotes
                .Select(GuildEmoteAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        IReadOnlyCollection<GuildEmote> IGuild.Emotes
            => SocketGuild.Emotes;

        /// <inheritdoc />
        public ISocketRole EveryoneRole
            => SocketGuild.EveryoneRole
                .Abstract();

        /// <inheritdoc />
        IRole IGuild.EveryoneRole
            => (SocketGuild as IGuild).EveryoneRole;

        /// <inheritdoc />
        public ExplicitContentFilterLevel ExplicitContentFilter
            => SocketGuild.ExplicitContentFilter;

        /// <inheritdoc />
        public IReadOnlyCollection<string> Features
            => SocketGuild.Features;

        /// <inheritdoc />
        public bool HasAllMembers
            => SocketGuild.HasAllMembers;

        /// <inheritdoc />
        public string IconId
            => SocketGuild.IconId;

        /// <inheritdoc />
        public string IconUrl
            => SocketGuild.IconUrl;

        /// <inheritdoc />
        public ulong Id
            => SocketGuild.Id;

        /// <inheritdoc />
        public bool IsConnected
            => SocketGuild.IsConnected;

        /// <inheritdoc />
        public bool IsEmbeddable
            => SocketGuild.IsEmbeddable;

        /// <inheritdoc />
        public bool IsSynced
            => SocketGuild.IsSynced;

        /// <inheritdoc />
        public int MemberCount
            => SocketGuild.MemberCount;

        /// <inheritdoc />
        public MfaLevel MfaLevel
            => SocketGuild.MfaLevel;

        /// <inheritdoc />
        public string Name
            => SocketGuild.Name;

        /// <inheritdoc />
        public ISocketGuildUser Owner
            => SocketGuild.Owner
                .Abstract();

        /// <inheritdoc />
        public ulong OwnerId
            => (SocketGuild as IGuild).OwnerId;

        /// <inheritdoc />
        public IReadOnlyCollection<ISocketRole> Roles
            => SocketGuild.Roles
                .Select(SocketRoleAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        IReadOnlyCollection<IRole> IGuild.Roles
            => (SocketGuild as IGuild).Roles;

        /// <inheritdoc />
        public IReadOnlyCollection<ISocketGuildUser> Users
            => SocketGuild.Users
                .Select(SocketGuildUserAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        public string SplashId
            => SocketGuild.SplashId;

        /// <inheritdoc />
        public string SplashUrl
            => SocketGuild.SplashUrl;

        /// <inheritdoc />
        public Task SyncPromise
            => SocketGuild.SyncPromise;

        /// <inheritdoc />
        public ISocketTextChannel SystemChannel
            => SocketGuild.SystemChannel
                .Abstract();

        /// <inheritdoc />
        public ulong? SystemChannelId
            => (SocketGuild as IGuild).SystemChannelId;

        /// <inheritdoc />
        public IReadOnlyCollection<ISocketTextChannel> TextChannels
            => SocketGuild.TextChannels
                .Select(SocketTextChannelAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        public VerificationLevel VerificationLevel
            => SocketGuild.VerificationLevel;

        /// <inheritdoc />
        public IReadOnlyCollection<ISocketVoiceChannel> VoiceChannels
            => SocketGuild.VoiceChannels
                .Select(SocketVoiceChannelAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        public string VoiceRegionId
            => SocketGuild.VoiceRegionId;

        /// <inheritdoc />
        public Task AddBanAsync(ulong userId, int pruneDays = 0, string reason = null, RequestOptions options = null)
            => SocketGuild.AddBanAsync(userId, pruneDays, reason, options);

        /// <inheritdoc />
        public Task AddBanAsync(IUser user, int pruneDays = 0, string reason = null, RequestOptions options = null)
            => SocketGuild.AddBanAsync(user, pruneDays, reason, options);

        /// <inheritdoc />
        public async Task<IRestGuildUser> AddGuildUserAsync(ulong id, string accessToken, Action<AddGuildUserProperties> func = null, RequestOptions options = null)
            => (await SocketGuild.AddGuildUserAsync(id, accessToken, func, options))
                .Abstract();

        /// <inheritdoc />
        Task<IGuildUser> IGuild.AddGuildUserAsync(ulong userId, string accessToken, Action<AddGuildUserProperties> func, RequestOptions options)
            => (SocketGuild as IGuild).AddGuildUserAsync(userId, accessToken, func, options);

        /// <inheritdoc />
        public async Task<IRestCategoryChannel> CreateCategoryChannelAsync(string name, RequestOptions options = null)
            => (await SocketGuild.CreateCategoryChannelAsync(name, options))
                .Abstract();

        /// <inheritdoc />
        public Task<ICategoryChannel> CreateCategoryAsync(string name, RequestOptions options = null)
            => (SocketGuild as IGuild).CreateCategoryAsync(name, options);

        /// <inheritdoc />
        public async Task<IGuildEmote> CreateEmoteAsync(string name, Image image, Optional<IEnumerable<IRole>> roles = default, RequestOptions options = null)
            => (await SocketGuild.CreateEmoteAsync(name, image, roles, options))
                .Abstract();

        /// <inheritdoc />
        Task<GuildEmote> IGuild.CreateEmoteAsync(string name, Image image, Optional<IEnumerable<IRole>> roles, RequestOptions options)
            => (SocketGuild as IGuild).CreateEmoteAsync(name, image, roles, options);

        /// <inheritdoc />
        public async Task<IRestGuildIntegration> CreateIntegrationAsync(ulong id, string type, RequestOptions options = null)
            => (await SocketGuild.CreateIntegrationAsync(id, type, options))
                .Abstract();

        /// <inheritdoc />
        Task<IGuildIntegration> IGuild.CreateIntegrationAsync(ulong id, string type, RequestOptions options)
            => (SocketGuild as IGuild).CreateIntegrationAsync(id, type, options);

        /// <inheritdoc />
        public async Task<IRestRole> CreateRoleAsync(string name, GuildPermissions? permissions = null, Color? color = null, bool isHoisted = false, RequestOptions options = null)
            => (await SocketGuild.CreateRoleAsync(name, permissions, color, isHoisted, options))
                .Abstract();

        /// <inheritdoc />
        Task<IRole> IGuild.CreateRoleAsync(string name, GuildPermissions? permissions, Color? color, bool isHoisted, RequestOptions options)
            => (SocketGuild as IGuild).CreateRoleAsync(name, permissions, color, isHoisted, options);

        /// <inheritdoc />
        public async Task<IRestTextChannel> CreateTextChannelAsync(string name, Action<TextChannelProperties> func = null, RequestOptions options = null)
            => (await SocketGuild.CreateTextChannelAsync(name, func, options))
                .Abstract();

        /// <inheritdoc />
        Task<ITextChannel> IGuild.CreateTextChannelAsync(string name, Action<TextChannelProperties> func, RequestOptions options)
            => (SocketGuild as IGuild).CreateTextChannelAsync(name, func, options);

        /// <inheritdoc />
        public async Task<IRestVoiceChannel> CreateVoiceChannelAsync(string name, Action<VoiceChannelProperties> func = null, RequestOptions options = null)
            => (await SocketGuild.CreateVoiceChannelAsync(name, func, options))
                .Abstract();

        /// <inheritdoc />
        Task<IVoiceChannel> IGuild.CreateVoiceChannelAsync(string name, Action<VoiceChannelProperties> func, RequestOptions options)
            => (SocketGuild as IGuild).CreateVoiceChannelAsync(name, func, options);

        /// <inheritdoc />
        public Task DeleteAsync(RequestOptions options = null)
            => SocketGuild.DeleteAsync(options);

        /// <inheritdoc />
        public Task DeleteEmoteAsync(IGuildEmote emote, RequestOptions options = null)
            => SocketGuild.DeleteEmoteAsync(emote.Unabstract());

        /// <inheritdoc />
        public Task DeleteEmoteAsync(GuildEmote emote, RequestOptions options = null)
            => SocketGuild.DeleteEmoteAsync(emote);

        /// <inheritdoc />
        public Task DownloadUsersAsync()
            => SocketGuild.DownloadUsersAsync();

        /// <inheritdoc />
        public Task<IVoiceChannel> GetAFKChannelAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetAFKChannelAsync(mode, options);

        /// <inheritdoc />
        public IAsyncEnumerable<IReadOnlyCollection<IRestAuditLogEntry>> GetAuditLogsAsync(int limit, RequestOptions options = null)
            => SocketGuild.GetAuditLogsAsync(limit, options)
                .Select(x => x
                    .Select(RestAuditLogEntryAbstractionExtensions.Abstract)
                    .ToArray());

        /// <inheritdoc />
        public Task<IReadOnlyCollection<IAuditLogEntry>> GetAuditLogsAsync(int limit = 100, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetAuditLogsAsync(limit, mode, options);

        /// <inheritdoc />
        public async Task<IBan> GetBanAsync(ulong userId, RequestOptions options = null)
            => await SocketGuild.GetBanAsync(userId, options);

        /// <inheritdoc />
        public async Task<IBan> GetBanAsync(IUser user, RequestOptions options = null)
            => await SocketGuild.GetBanAsync(user, options);

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<IBan>> GetBansAsync(RequestOptions options = null)
            => await SocketGuild.GetBansAsync(options);

        /// <inheritdoc />
        public Task<IReadOnlyCollection<ICategoryChannel>> GetCategoriesAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetCategoriesAsync(mode, options);

        /// <inheritdoc />
        public ISocketGuildChannel GetChannel(ulong id)
            => SocketGuild.GetChannel(id)
                .Abstract();

        /// <inheritdoc />
        public Task<IGuildChannel> GetChannelAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetChannelAsync(id, mode, options);

        /// <inheritdoc />
        public Task<IReadOnlyCollection<IGuildChannel>> GetChannelsAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetChannelsAsync(mode, options);

        /// <inheritdoc />
        public Task<IGuildUser> GetCurrentUserAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetCurrentUserAsync(mode, options);

        /// <inheritdoc />
        public Task<ITextChannel> GetDefaultChannelAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetDefaultChannelAsync(mode, options);

        /// <inheritdoc />
        public Task<IGuildChannel> GetEmbedChannelAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetEmbedChannelAsync(mode, options);

        /// <inheritdoc />
        public async Task<IGuildEmote> GetEmoteAsync(ulong id, RequestOptions options = null)
            => (await SocketGuild.GetEmoteAsync(id, options))
                .Abstract();

        /// <inheritdoc />
        Task<GuildEmote> IGuild.GetEmoteAsync(ulong id, RequestOptions options)
            => (SocketGuild as IGuild).GetEmoteAsync(id, options);

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<IRestGuildIntegration>> GetIntegrationsAsync(RequestOptions options = null)
            => (await SocketGuild.GetIntegrationsAsync(options))
                .Select(RestGuildIntegrationAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        Task<IReadOnlyCollection<IGuildIntegration>> IGuild.GetIntegrationsAsync(RequestOptions options)
            => (SocketGuild as IGuild).GetIntegrationsAsync(options);

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<IRestInviteMetadata>> GetInvitesAsync(RequestOptions options = null)
            => (await SocketGuild.GetInvitesAsync(options))
                .Select(RestInviteMetadataAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        Task<IReadOnlyCollection<IInviteMetadata>> IGuild.GetInvitesAsync(RequestOptions options)
            => (SocketGuild as IGuild).GetInvitesAsync(options);

        /// <inheritdoc />
        public Task<IGuildUser> GetOwnerAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetOwnerAsync(mode, options);

        /// <inheritdoc />
        public ISocketRole GetRole(ulong id)
            => SocketGuild.GetRole(id)
                .Abstract();

        /// <inheritdoc />
        IRole IGuild.GetRole(ulong id)
            => (SocketGuild as IGuild).GetRole(id);

        /// <inheritdoc />
        public Task<ITextChannel> GetSystemChannelAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetSystemChannelAsync(mode, options);

        /// <inheritdoc />
        public ISocketTextChannel GetTextChannel(ulong id)
            => SocketGuild.GetTextChannel(id)
                .Abstract();

        /// <inheritdoc />
        public Task<ITextChannel> GetTextChannelAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetTextChannelAsync(id, mode, options);

        /// <inheritdoc />
        public Task<IReadOnlyCollection<ITextChannel>> GetTextChannelsAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetTextChannelsAsync(mode, options);

        /// <inheritdoc />
        public ISocketGuildUser GetUser(ulong id)
            => SocketGuild.GetUser(id)
                .Abstract();

        /// <inheritdoc />
        public Task<IGuildUser> GetUserAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetUserAsync(id, mode, options);

        /// <inheritdoc />
        public Task<IReadOnlyCollection<IGuildUser>> GetUsersAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetUsersAsync(mode, options);

        /// <inheritdoc />
        public async Task<IRestInviteMetadata> GetVanityInviteAsync(RequestOptions options = null)
            => (await SocketGuild.GetVanityInviteAsync(options))
                .Abstract();

        /// <inheritdoc />
        Task<IInviteMetadata> IGuild.GetVanityInviteAsync(RequestOptions options)
            => (SocketGuild as IGuild).GetVanityInviteAsync(options);

        /// <inheritdoc />
        public ISocketVoiceChannel GetVoiceChannel(ulong id)
            => SocketGuild.GetVoiceChannel(id)
                .Abstract();

        /// <inheritdoc />
        public Task<IVoiceChannel> GetVoiceChannelAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetVoiceChannelAsync(id, mode, options);

        /// <inheritdoc />
        public Task<IReadOnlyCollection<IVoiceChannel>> GetVoiceChannelsAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => (SocketGuild as IGuild).GetVoiceChannelsAsync(mode, options);

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<IRestVoiceRegion>> GetVoiceRegionsAsync(RequestOptions options = null)
            => (await SocketGuild.GetVoiceRegionsAsync(options))
                .Select(RestVoiceRegionAbsractionExtension.Abstract)
                .ToArray();

        /// <inheritdoc />
        Task<IReadOnlyCollection<IVoiceRegion>> IGuild.GetVoiceRegionsAsync(RequestOptions options)
            => (SocketGuild as IGuild).GetVoiceRegionsAsync(options);

        /// <inheritdoc />
        public async Task<IRestWebhook> GetWebhookAsync(ulong id, RequestOptions options = null)
            => (await SocketGuild.GetWebhookAsync(id, options))
                .Abstract();

        /// <inheritdoc />
        Task<IWebhook> IGuild.GetWebhookAsync(ulong id, RequestOptions options)
            => (SocketGuild as IGuild).GetWebhookAsync(id, options);

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<IRestWebhook>> GetWebhooksAsync(RequestOptions options = null)
            => (await SocketGuild.GetWebhooksAsync(options))
                .Select(RestWebhookAbstractionExtensions.Abstract)
                .ToArray();

        /// <inheritdoc />
        Task<IReadOnlyCollection<IWebhook>> IGuild.GetWebhooksAsync(RequestOptions options)
            => (SocketGuild as IGuild).GetWebhooksAsync(options);

        /// <inheritdoc />
        public Task LeaveAsync(RequestOptions options = null)
            => SocketGuild.LeaveAsync(options);

        /// <inheritdoc />
        public Task ModifyAsync(Action<GuildProperties> func, RequestOptions options = null)
            => SocketGuild.ModifyAsync(func, options);

        /// <inheritdoc />
        public Task ModifyEmbedAsync(Action<GuildEmbedProperties> func, RequestOptions options = null)
            => SocketGuild.ModifyEmbedAsync(func, options);

        /// <inheritdoc />
        public async Task<IGuildEmote> ModifyEmoteAsync(IGuildEmote emote, Action<EmoteProperties> func, RequestOptions options = null)
            => (await SocketGuild.ModifyEmoteAsync(emote.Unabstract(), func, options))
                .Abstract();

        /// <inheritdoc />
        Task<GuildEmote> IGuild.ModifyEmoteAsync(GuildEmote emote, Action<EmoteProperties> func, RequestOptions options)
            => (SocketGuild as IGuild).ModifyEmoteAsync(emote, func, options);

        /// <inheritdoc />
        public Task<int> PruneUsersAsync(int days = 30, bool simulate = false, RequestOptions options = null)
            => SocketGuild.PruneUsersAsync(days, simulate, options);

        /// <inheritdoc />
        public Task RemoveBanAsync(ulong userId, RequestOptions options = null)
            => SocketGuild.RemoveBanAsync(userId, options);

        /// <inheritdoc />
        public Task RemoveBanAsync(IUser user, RequestOptions options = null)
            => SocketGuild.RemoveBanAsync(user, options);

        /// <inheritdoc />
        public Task ReorderChannelsAsync(IEnumerable<ReorderChannelProperties> args, RequestOptions options = null)
            => SocketGuild.ReorderChannelsAsync(args, options);

        /// <inheritdoc />
        public Task ReorderRolesAsync(IEnumerable<ReorderRoleProperties> args, RequestOptions options = null)
            => SocketGuild.ReorderRolesAsync(args, options);

        /// <inheritdoc cref="SocketGuild.ToString" />
        public override string ToString()
            => SocketGuild.ToString();

        /// <summary>
        /// The existing <see cref="Rest.SocketGuild"/> being abstracted.
        /// </summary>
        protected SocketGuild SocketGuild { get; }
    }

    /// <summary>
    /// Contains extension methods for abstracting <see cref="SocketGuild"/> objects.
    /// </summary>
    public static class SocketGuildAbstractionExtensions
    {
        /// <summary>
        /// Converts an existing <see cref="SocketGuild"/> to an abstracted <see cref="ISocketGuild"/> value.
        /// </summary>
        /// <param name="socketGuild">The existing <see cref="SocketGuild"/> to be abstracted.</param>
        /// <exception cref="ArgumentNullException">Throws for <paramref name="socketGuild"/>.</exception>
        /// <returns>An <see cref="ISocketGuild"/> that abstracts <paramref name="socketGuild"/>.</returns>
        public static ISocketGuild Abstract(this SocketGuild socketGuild)
            => new SocketGuildAbstraction(socketGuild);
    }
}
