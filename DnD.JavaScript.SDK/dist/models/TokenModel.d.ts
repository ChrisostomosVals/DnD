export default interface TokenModel {
    access_token: string;
    expires_in: number;
    refresh_token: string;
    scope: string;
}
