"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const ErrorResponseModel_1 = __importDefault(require("./ErrorResponseModel"));
class TokenErrorResponseModel extends ErrorResponseModel_1.default {
    constructor(error, error_description, exception, name) {
        super(error, error_description, exception, name);
        this.error_description = error_description;
    }
    static NewErrorMsg(error, message) {
        return new TokenErrorResponseModel(error, message, null, null);
    }
    static NewErrorExMsg(error, message, exception) {
        return new TokenErrorResponseModel(error, message, exception, exception.name);
    }
    static NewError(error, exception) {
        return new TokenErrorResponseModel(error, exception.message, exception, exception.name);
    }
}
exports.default = TokenErrorResponseModel;
//# sourceMappingURL=TokenErrorResponseModel.js.map