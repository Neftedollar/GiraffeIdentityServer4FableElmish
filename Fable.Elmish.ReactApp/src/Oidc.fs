module rec Oidc

open System
open Fable.Core
open Fable.Import.JS


type [<AllowNullLiteral>] IExports =
    abstract InMemoryWebStorage: InMemoryWebStorageStatic
    abstract Log: LogStatic
    abstract MetadataService: MetadataServiceStatic
    abstract OidcClient: OidcClientStatic
    abstract UserManager: UserManagerStatic
    abstract WebStorageStateStore: WebStorageStateStoreStatic
    abstract SigninResponse: SigninResponseStatic
    abstract SignoutResponse: SignoutResponseStatic
    abstract User: UserStatic
    abstract CordovaPopupWindow: CordovaPopupWindowStatic
    abstract CordovaPopupNavigator: CordovaPopupNavigatorStatic
    abstract CordovaIFrameNavigator: CordovaIFrameNavigatorStatic
    abstract CheckSessionIFrame: CheckSessionIFrameStatic
    abstract SessionMonitor: SessionMonitorStatic

type [<AllowNullLiteral>] Logger =
    abstract error: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit
    abstract info: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit
    abstract debug: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit
    abstract warn: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit

type [<AllowNullLiteral>] AccessTokenEvents =
    abstract load: container: User -> unit
    abstract unload: unit -> unit
    abstract addAccessTokenExpiring: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract removeAccessTokenExpiring: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract addAccessTokenExpired: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract removeAccessTokenExpired: callback: (ResizeArray<obj option> -> unit) -> unit

type [<AllowNullLiteral>] InMemoryWebStorage =
    abstract getItem: key: string -> obj option
    abstract setItem: key: string * value: obj option -> obj option
    abstract removeItem: key: string -> obj option
    abstract key: index: float -> obj option
    abstract length: float option with get, set

type [<AllowNullLiteral>] InMemoryWebStorageStatic =
    [<Emit "new $0($1...)">] abstract Create: unit -> InMemoryWebStorage

type [<AllowNullLiteral>] Log =
    interface end

type [<AllowNullLiteral>] LogStatic =
    [<Emit "new $0($1...)">] abstract Create: unit -> Log
    abstract NONE: float
    abstract ERROR: float
    abstract WARN: float
    abstract INFO: float
    abstract DEBUG: float
    abstract reset: unit -> unit
    abstract level: float with get, set
    abstract logger: Logger with get, set
    abstract debug: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit
    abstract info: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit
    abstract warn: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit
    abstract error: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit

type [<AllowNullLiteral>] MetadataService =
    abstract metadataUrl: string option with get, set
    abstract getMetadata: unit -> Promise<OidcMetadata>
    abstract getIssuer: unit -> Promise<string>
    abstract getAuthorizationEndpoint: unit -> Promise<string>
    abstract getUserInfoEndpoint: unit -> Promise<string>
    abstract getTokenEndpoint: unit -> Promise<string option>
    abstract getCheckSessionIframe: unit -> Promise<string option>
    abstract getEndSessionEndpoint: unit -> Promise<string option>
    abstract getRevocationEndpoint: unit -> Promise<string option>
    abstract getKeysEndpoint: unit -> Promise<string option>
    abstract getSigningKeys: unit -> Promise<ResizeArray<obj option>>

type [<AllowNullLiteral>] MetadataServiceStatic =
    [<Emit "new $0($1...)">] abstract Create: settings: OidcClientSettings -> MetadataService

type [<AllowNullLiteral>] MetadataServiceCtor =
    [<Emit "$0($1...)">] abstract Invoke: settings: OidcClientSettings * ?jsonServiceCtor: obj -> MetadataService

type [<AllowNullLiteral>] ResponseValidator =
    abstract validateSigninResponse: state: obj option * response: obj option -> Promise<SigninResponse>
    abstract validateSignoutResponse: state: obj option * response: obj option -> Promise<SignoutResponse>

type [<AllowNullLiteral>] ResponseValidatorCtor =
    [<Emit "$0($1...)">] abstract Invoke: settings: OidcClientSettings * ?metadataServiceCtor: MetadataServiceCtor * ?userInfoServiceCtor: obj -> ResponseValidator

type [<AllowNullLiteral>] SigninRequest =
    abstract url: string with get, set
    abstract state: obj option with get, set

type [<AllowNullLiteral>] SignoutRequest =
    abstract url: string with get, set
    abstract state: obj option with get, set

type [<AllowNullLiteral>] OidcClient =
    abstract settings: OidcClientSettings
    abstract createSigninRequest: ?args: obj -> Promise<SigninRequest>
    abstract processSigninResponse: ?url: string * ?stateStore: StateStore -> Promise<SigninResponse>
    abstract createSignoutRequest: ?args: obj -> Promise<SignoutRequest>
    abstract processSignoutResponse: ?url: string * ?stateStore: StateStore -> Promise<SignoutResponse>
    abstract clearStaleState: stateStore: StateStore -> Promise<obj option>
    abstract metadataService: MetadataService

type [<AllowNullLiteral>] OidcClientStatic =
    [<Emit "new $0($1...)">] abstract Create: settings: OidcClientSettings -> OidcClient

type [<AllowNullLiteral>] OidcClientSettings =
    abstract authority: string option with get, set
    abstract metadataUrl: string option
    abstract metadata: obj option with get, set
    abstract signingKeys: ResizeArray<obj option> option with get, set
    abstract client_id: string option with get, set
    abstract client_secret: string option with get, set
    abstract response_type: string option with get, set
    abstract response_mode: string option
    abstract scope: string option with get, set
    abstract redirect_uri: string option with get, set
    abstract post_logout_redirect_uri: string option  with get, set
    abstract popup_post_logout_redirect_uri: string option
    abstract prompt: string option
    abstract display: string option
    abstract max_age: float option
    abstract ui_locales: string option
    abstract acr_values: string option
    abstract filterProtocolClaims: bool option
    abstract loadUserInfo: bool option
    abstract staleStateAge: float option
    abstract clockSkew: float option
    abstract stateStore: StateStore option
    abstract userInfoJwtIssuer: U3<string, string, string> option
    abstract ResponseValidatorCtor: ResponseValidatorCtor option with get, set
    abstract MetadataServiceCtor: MetadataServiceCtor option with get, set
    abstract extraQueryParams: TypeLiteral_01 option with get, set

type [<AllowNullLiteral>] UserManager =
    inherit OidcClient
    abstract settings: UserManagerSettings
    abstract clearStaleState: unit -> Promise<unit>
    abstract getUser: unit -> Promise<User option>
    abstract storeUser: user: User -> Promise<unit>
    abstract removeUser: unit -> Promise<unit>
    abstract signinPopup: ?args: obj -> Promise<User>
    abstract signinPopupCallback: ?url: string -> Promise<obj option>
    abstract signinSilent: ?args: obj -> Promise<User>
    abstract signinSilentCallback: ?url: string -> Promise<obj option>
    abstract signinRedirect: ?args: obj -> Promise<obj option>
    abstract signinRedirectCallback: ?url: string -> Promise<User>
    abstract signoutRedirect: ?args: obj -> Promise<obj option>
    abstract signoutRedirectCallback: ?url: string -> Promise<obj option>
    abstract signoutPopup: ?args: obj -> Promise<obj option>
    abstract signoutPopupCallback: ?url: string * ?keepOpen: bool -> Promise<unit>
    abstract signoutPopupCallback: ?keepOpen: bool -> Promise<unit>
    abstract querySessionStatus: ?args: obj -> Promise<obj option>
    abstract revokeAccessToken: unit -> Promise<unit>
    abstract startSilentRenew: unit -> unit
    abstract stopSilentRenew: unit -> unit
    abstract events: UserManagerEvents with get, set

type [<AllowNullLiteral>] UserManagerStatic =
    [<Emit "new $0($1...)">] abstract Create: settings: UserManagerSettings -> UserManager

type [<AllowNullLiteral>] UserManagerEvents =
    inherit AccessTokenEvents
    abstract load: user: User -> obj option
    abstract unload: unit -> obj option
    abstract addUserLoaded: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract removeUserLoaded: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract addUserUnloaded: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract removeUserUnloaded: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract addSilentRenewError: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract removeSilentRenewError: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract addUserSignedOut: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract removeUserSignedOut: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract addUserSessionChanged: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract removeUserSessionChanged: callback: (ResizeArray<obj option> -> unit) -> unit

type [<AllowNullLiteral>] UserManagerSettings =
    inherit OidcClientSettings
    abstract popup_redirect_uri: string option
    abstract popupWindowFeatures: string option
    abstract popupWindowTarget: obj option
    abstract silent_redirect_uri: obj option
    abstract silentRequestTimeout: obj option
    abstract automaticSilentRenew: bool option
    abstract includeIdTokenInSilentRenew: bool option
    abstract monitorSession: bool option
    abstract checkSessionInterval: float option
    abstract query_status_response_type: string option
    abstract stopCheckSessionOnError: bool option
    abstract revokeAccessTokenOnSignout: obj option
    abstract accessTokenExpiringNotificationTime: float option
    abstract redirectNavigator: obj option
    abstract popupNavigator: obj option
    abstract iframeNavigator: obj option
    abstract userStore: obj option

type [<AllowNullLiteral>] WebStorageStateStoreSettings =
    abstract prefix: string option with get, set
    abstract store: obj option with get, set

type [<AllowNullLiteral>] StateStore =
    abstract set: key: string * value: obj option -> Promise<unit>
    abstract get: key: string -> Promise<obj option>
    abstract remove: key: string -> Promise<obj option>
    abstract getAllKeys: unit -> Promise<ResizeArray<string>>

type [<AllowNullLiteral>] WebStorageStateStore =
    inherit StateStore
    abstract set: key: string * value: obj option -> Promise<unit>
    abstract get: key: string -> Promise<obj option>
    abstract remove: key: string -> Promise<obj option>
    abstract getAllKeys: unit -> Promise<ResizeArray<string>>

type [<AllowNullLiteral>] WebStorageStateStoreStatic =
    [<Emit "new $0($1...)">] abstract Create: settings: WebStorageStateStoreSettings -> WebStorageStateStore

type [<AllowNullLiteral>] SigninResponse =
    abstract access_token: string with get, set
    abstract code: string with get, set
    abstract error: string with get, set
    abstract error_description: string with get, set
    abstract error_uri: string with get, set
    abstract id_token: string with get, set
    abstract profile: obj option with get, set
    abstract scope: string with get, set
    abstract session_state: obj option with get, set
    abstract state: obj option with get, set
    abstract token_type: string with get, set
    abstract expired: bool option
    abstract expires_in: float option
    abstract isOpenIdConnect: bool
    abstract scopes: ResizeArray<string>

type [<AllowNullLiteral>] SigninResponseStatic =
    [<Emit "new $0($1...)">] abstract Create: url: string * ?delimiter: string -> SigninResponse

type [<AllowNullLiteral>] SignoutResponse =
    abstract error: string with get, set
    abstract error_description: string with get, set
    abstract error_uri: string with get, set
    abstract state: obj option with get, set

type [<AllowNullLiteral>] SignoutResponseStatic =
    [<Emit "new $0($1...)">] abstract Create: url: string -> SignoutResponse

type [<AllowNullLiteral>] UserSettings =
    abstract id_token: string with get, set
    abstract session_state: obj option with get, set
    abstract access_token: string with get, set
    abstract refresh_token: string with get, set
    abstract token_type: string with get, set
    abstract scope: string with get, set
    abstract profile: obj option with get, set
    abstract expires_at: float with get, set
    abstract state: obj option with get, set

type [<AllowNullLiteral>] User =
    abstract id_token: string with get, set
    abstract session_state: obj option with get, set
    abstract access_token: string with get, set
    abstract refresh_token: string with get, set
    abstract token_type: string with get, set
    abstract scope: string with get, set
    abstract profile: obj option with get, set
    abstract expires_at: float with get, set
    abstract state: obj option with get, set
    abstract toStorageString: unit -> string
    abstract expires_in: float option
    abstract expired: bool option
    abstract scopes: ResizeArray<string>

type [<AllowNullLiteral>] UserStatic =
    [<Emit "new $0($1...)">] abstract Create: settings: UserSettings -> User

type [<AllowNullLiteral>] CordovaPopupWindow =
    abstract navigate: ``params``: obj option -> Promise<obj option>
    abstract promise: Promise<obj option> with get, set

type [<AllowNullLiteral>] CordovaPopupWindowStatic =
    [<Emit "new $0($1...)">] abstract Create: ``params``: obj option -> CordovaPopupWindow

type [<AllowNullLiteral>] CordovaPopupNavigator =
    abstract prepare: ``params``: obj option -> Promise<CordovaPopupWindow>

type [<AllowNullLiteral>] CordovaPopupNavigatorStatic =
    [<Emit "new $0($1...)">] abstract Create: unit -> CordovaPopupNavigator

type [<AllowNullLiteral>] CordovaIFrameNavigator =
    abstract prepare: ``params``: obj option -> Promise<CordovaPopupWindow>

type [<AllowNullLiteral>] CordovaIFrameNavigatorStatic =
    [<Emit "new $0($1...)">] abstract Create: unit -> CordovaIFrameNavigator

type [<AllowNullLiteral>] OidcMetadata =
    abstract issuer: string with get, set
    abstract authorization_endpoint: string with get, set
    abstract token_endpoint: string with get, set
    abstract token_endpoint_auth_methods_supported: ResizeArray<string> with get, set
    abstract token_endpoint_auth_signing_alg_values_supported: ResizeArray<string> with get, set
    abstract userinfo_endpoint: string with get, set
    abstract check_session_iframe: string with get, set
    abstract end_session_endpoint: string with get, set
    abstract jwks_uri: string with get, set
    abstract registration_endpoint: string with get, set
    abstract scopes_supported: ResizeArray<string> with get, set
    abstract response_types_supported: ResizeArray<string> with get, set
    abstract acr_values_supported: ResizeArray<string> with get, set
    abstract subject_types_supported: ResizeArray<string> with get, set
    abstract userinfo_signing_alg_values_supported: ResizeArray<string> with get, set
    abstract userinfo_encryption_alg_values_supported: ResizeArray<string> with get, set
    abstract userinfo_encryption_enc_values_supported: ResizeArray<string> with get, set
    abstract id_token_signing_alg_values_supported: ResizeArray<string> with get, set
    abstract id_token_encryption_alg_values_supported: ResizeArray<string> with get, set
    abstract id_token_encryption_enc_values_supported: ResizeArray<string> with get, set
    abstract request_object_signing_alg_values_supported: ResizeArray<string> with get, set
    abstract display_values_supported: ResizeArray<string> with get, set
    abstract claim_types_supported: ResizeArray<string> with get, set
    abstract claims_supported: ResizeArray<string> with get, set
    abstract claims_parameter_supported: bool with get, set
    abstract service_documentation: string with get, set
    abstract ui_locales_supported: ResizeArray<string> with get, set
    abstract revocation_endpoint: string with get, set
    abstract introspection_endpoint: string with get, set
    abstract frontchannel_logout_supported: bool with get, set
    abstract frontchannel_logout_session_supported: bool with get, set
    abstract backchannel_logout_supported: bool with get, set
    abstract backchannel_logout_session_supported: bool with get, set
    abstract grant_types_supported: ResizeArray<string> with get, set
    abstract response_modes_supported: ResizeArray<string> with get, set
    abstract code_challenge_methods_supported: ResizeArray<string> with get, set

type [<AllowNullLiteral>] CheckSessionIFrame =
    abstract load: unit -> Promise<unit>
    abstract start: session_state: string -> unit
    abstract stop: unit -> unit

type [<AllowNullLiteral>] CheckSessionIFrameStatic =
    [<Emit "new $0($1...)">] abstract Create: callback: (unit -> unit) * client_id: string * url: string * ?interval: float * ?stopOnError: bool -> CheckSessionIFrame

type [<AllowNullLiteral>] CheckSessionIFrameCtor =
    [<Emit "$0($1...)">] abstract Invoke: callback: (unit -> unit) * client_id: string * url: string * ?interval: float * ?stopOnError: bool -> CheckSessionIFrame

type [<AllowNullLiteral>] SessionMonitor =
    interface end

type [<AllowNullLiteral>] SessionMonitorStatic =
    [<Emit "new $0($1...)">] abstract Create: userManager: UserManager * CheckSessionIFrameCtor: CheckSessionIFrameCtor -> SessionMonitor

type [<AllowNullLiteral>] TypeLiteral_01 =
    interface end
