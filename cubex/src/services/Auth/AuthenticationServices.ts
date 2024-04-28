import { UserManager, UserManagerSettings } from 'oidc-client';

export default class AuthenticationService {
  userManager?: UserManager;
  settings?: UserManagerSettings;

  constructor() {
    this.settings = {
      authority: process.env.IDENTITY_AUTHORITY,
      client_id: process.env.IDENTITY_CLIENT_ID,
      redirect_uri: window.location.origin + '/auth/callback',
      post_logout_redirect_uri: window.location.origin + '/auth/callback',
      response_type: 'id_token token',
      scope: 'openid profile budgets tenant roles',
      monitorSession: false,
    };

    if (this.settings) {
      this.userManager = new UserManager(this.settings);
    } else {
      console.log('No app settings found for Authentication Service.');
    }
  }

  getUser() {
    return this.userManager?.getUser() ?? null;
  }

  login() {
    return this.userManager?.signinRedirect() ?? null;
  }

  loginSilent() {
    return this.userManager?.signinSilentCallback() ?? null;
  }

  loginCallback() {
    return this.userManager?.signinRedirectCallback() ?? null;
  }

  logout() {
    return this.userManager?.signoutRedirect() ?? null;
  }

  logoutCallback() {
    return this.userManager?.signoutRedirectCallback() ?? null;
  }

  getAccessToken() {
    return (
      this.userManager?.getUser().then((data) => {
        return data?.access_token ?? null;
      }) ?? null
    );
  }

  getIdToken() {
    return (
      this.userManager?.getUser().then((data) => {
        return data?.id_token ?? null;
      }) ?? null
    );
  }

  getUserRole() {
    return (
      this.userManager?.getUser().then((data) => {
        if (data) {
          return this.decodeJwtToken(data.access_token).role;
        }
        return null;
      }) ?? null
    );
  }

  decodeJwtToken(token: string) {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(function (c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join(''),
    );

    return JSON.parse(jsonPayload);
  }
}
