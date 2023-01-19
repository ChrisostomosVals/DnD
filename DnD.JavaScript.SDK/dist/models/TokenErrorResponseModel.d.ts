import ErrorResponseModel from "./ErrorResponseModel";
export default class TokenErrorResponseModel extends ErrorResponseModel {
    error_description: string | null;
    constructor(error: string, error_description: string, exception: Error | null, name: string | null);
    static NewErrorMsg(error: string, message: string): TokenErrorResponseModel;
    static NewErrorExMsg(error: string, message: string, exception: Error): TokenErrorResponseModel;
    static NewError(error: string, exception: Error): TokenErrorResponseModel;
}
