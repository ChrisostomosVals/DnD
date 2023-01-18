"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const ApiResponseModel_1 = __importDefault(require("../models/ApiResponseModel"));
const ErrorResponseModel_1 = __importDefault(require("../models/ErrorResponseModel"));
const constants_1 = require("../utils/constants");
const httpService_1 = __importDefault(require("../utils/httpService"));
class UserApi {
    static GetAsync(token, url) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                const uri = `${url}/${constants_1.userEndpoint}`;
                const response = yield httpService_1.default.getAsync(token, uri);
                if (response.ok) {
                    const data = yield response.json();
                    if (data === null) {
                        return new ApiResponseModel_1.default(data, ErrorResponseModel_1.default.NewErrorMsg("content-null", "The response body was empty"));
                    }
                    return new ApiResponseModel_1.default(data, null);
                }
                else if (response.status == 400 || response.status == 404) {
                    const errorMsg = yield response.json();
                    const error = response.statusText;
                    return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewErrorMsg(error, errorMsg));
                }
                else if (response.status == 401) {
                    const error = response.statusText;
                    return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewErrorMsg(error, "Unauthorized access"));
                }
            }
            catch (error) {
                return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewError("UserApi.GetAsync().Exception", error));
                ;
            }
        });
    }
    static GetByIdAsync(token, url, id) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                const uri = `${url}/${constants_1.userEndpoint}/${id}`;
                const response = yield httpService_1.default.getAsync(token, uri);
                if (response.ok) {
                    const data = yield response.json();
                    if (data === null) {
                        return new ApiResponseModel_1.default(data, ErrorResponseModel_1.default.NewErrorMsg("content-null", "The response body was empty"));
                    }
                    return new ApiResponseModel_1.default(data, null);
                }
                else if (response.status == 400 || response.status == 404) {
                    const errorMsg = yield response.json();
                    const error = response.statusText;
                    return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewErrorMsg(error, errorMsg));
                }
                else if (response.status == 401) {
                    const error = response.statusText;
                    return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewErrorMsg(error, "Unauthorized access"));
                }
            }
            catch (error) {
                return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewError("UserApi.GetByIdAsync().Exception", error));
                ;
            }
        });
    }
    static GetProfileAsync(token, url) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                const uri = `${url}/${constants_1.userEndpoint}/profile`;
                const response = yield httpService_1.default.getAsync(token, uri);
                if (response.ok) {
                    const data = yield response.json();
                    if (data === null) {
                        return new ApiResponseModel_1.default(data, ErrorResponseModel_1.default.NewErrorMsg("content-null", "The response body was empty"));
                    }
                    return new ApiResponseModel_1.default(data, null);
                }
                else if (response.status == 400 || response.status == 404) {
                    const errorMsg = yield response.json();
                    const error = response.statusText;
                    return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewErrorMsg(error, errorMsg));
                }
                else if (response.status == 401) {
                    const error = response.statusText;
                    return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewErrorMsg(error, "Unauthorized access"));
                }
            }
            catch (error) {
                return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewError("UserApi.GetByCategoryIdAsync().Exception", error));
                ;
            }
        });
    }
    static InsertAsync(token, url, request) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                const uri = `${url}/${constants_1.userEndpoint}`;
                const response = yield httpService_1.default.postAsync(token, uri, request);
                if (response.ok) {
                    const data = yield response.json();
                    if (data === null) {
                        return new ApiResponseModel_1.default(data, ErrorResponseModel_1.default.NewErrorMsg("content-null", "The response body was empty"));
                    }
                    return new ApiResponseModel_1.default(data, null);
                }
                else if (response.status == 400 || response.status == 404) {
                    const errorMsg = yield response.json();
                    const error = response.statusText;
                    return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewErrorMsg(error, errorMsg));
                }
                else if (response.status == 401) {
                    const error = response.statusText;
                    return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewErrorMsg(error, "Unauthorized access"));
                }
            }
            catch (error) {
                return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewError("UserApi.InsertAsync().Exception", error));
                ;
            }
        });
    }
    static UpdateAsync(token, url, request) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                const uri = `${url}/${constants_1.userEndpoint}`;
                const response = yield httpService_1.default.putAsync(token, uri, request);
                if (response.ok) {
                    const data = yield response.json();
                    if (data === null) {
                        return new ApiResponseModel_1.default(data, ErrorResponseModel_1.default.NewErrorMsg("content-null", "The response body was empty"));
                    }
                    return new ApiResponseModel_1.default(data, null);
                }
                else if (response.status == 400 || response.status == 404) {
                    const errorMsg = yield response.json();
                    const error = response.statusText;
                    return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewErrorMsg(error, errorMsg));
                }
                else if (response.status == 401) {
                    const error = response.statusText;
                    return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewErrorMsg(error, "Unauthorized access"));
                }
            }
            catch (error) {
                return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewError("UserApi.UpdateAsync().Exception", error));
                ;
            }
        });
    }
    static ChangePassword(token, url, id, request) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                const uri = `${url}/${constants_1.userEndpoint}/${id}/changepassword`;
                const response = yield httpService_1.default.patchAsync(token, uri, request);
                if (response.ok) {
                    const data = yield response.json();
                    if (data === null) {
                        return new ApiResponseModel_1.default(data, ErrorResponseModel_1.default.NewErrorMsg("content-null", "The response body was empty"));
                    }
                    return new ApiResponseModel_1.default(data, null);
                }
                else if (response.status == 400 || response.status == 404) {
                    const errorMsg = yield response.json();
                    const error = response.statusText;
                    return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewErrorMsg(error, errorMsg));
                }
                else if (response.status == 401) {
                    const error = response.statusText;
                    return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewErrorMsg(error, "Unauthorized access"));
                }
            }
            catch (error) {
                return new ApiResponseModel_1.default(null, ErrorResponseModel_1.default.NewError("UserApi.ChangePassword().Exception", error));
                ;
            }
        });
    }
}
exports.default = UserApi;
//# sourceMappingURL=UserApi.js.map