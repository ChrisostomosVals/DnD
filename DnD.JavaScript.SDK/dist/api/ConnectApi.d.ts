import ApiResponseModel from "../models/ApiResponseModel";
import TokenResponseModel from "../models/TokenResponseModel";
export default class ConnectApi {
    static LoginAsync(email: string, password: string, url: string): Promise<ApiResponseModel<TokenResponseModel>>;
}
