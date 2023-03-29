import * as jose from "jose";

const IsAuthenticated = async () => {
    let token = localStorage.getItem("token");
    if (token === null)
    {
        return false;
    }

    await jose.jwtVerify(token, new TextEncoder().encode('a'.repeat(256)));

    return true;
};

export {IsAuthenticated};
