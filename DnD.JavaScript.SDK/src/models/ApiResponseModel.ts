import ErrorResponseModel from './ErrorResponseModel'

export default class ApiResponseModel<T> {
    public data: T | null
    public error: ErrorResponseModel | null
    public isError: boolean
    constructor(data:T | null, error:ErrorResponseModel | null){
        this.isError = error !== null;
        if(this.isError){
            this.setError(error)
        }
        this.setModel(data);
        
    }

    private setModel(params: T) {
        this.data = params
    }

    private setError(params: ErrorResponseModel){
        this.error = params
    }
}
