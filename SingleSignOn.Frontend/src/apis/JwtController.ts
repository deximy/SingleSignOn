import {Global} from "./Global";

const GetJwtBySteamToken = async (steam_token: string) => {
    let headers = new Headers();
    headers.append("Content-Type", "application/json");

    let response = await fetch(
        `${Global.realm}/jwts`,
        {
            method: "POST",
            headers: headers,
            body: JSON.stringify(steam_token),
        }
    );
    if (!response.ok)
    {
        return false;
    }

    let token = await response.text();
    localStorage.setItem("token", token);
    return true;
};

export {GetJwtBySteamToken};
