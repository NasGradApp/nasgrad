import { authHeader } from "../helpers/auth.helpers";

export const defaultHeaders = () => {
    return {
        ...authHeader(),
        "Content-Type": "application/json"
    };
};

export const handleResponse = (response) => {
    return new Promise((resolve, reject) => {
        if (response.ok) {
            // return json if it was returned in the response
            const contentType = response.headers.get("Content-Type");
            if (contentType && contentType.includes("application/json")) {
                response.json().then(json => resolve(json));
            } else {
                resolve();
            }
        } else if (response.status === 401) {
            reject();
            window.handleHttp401();
        } else if (response.status === 403) {
            reject(response.statusText);
        } else {

            response.text().then(text => reject(text));
        }
    });
};
