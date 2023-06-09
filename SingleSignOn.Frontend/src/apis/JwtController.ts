import {Global} from "./Global";

const GetJwtBySteamToken = async (steam_token: string, redirect_url: string | null = null) => {
    let headers = new Headers();
    headers.append("Content-Type", "application/json");

    let response = await fetch(
        `${Global.realm}/jwts`,
        {
            method: "POST",
            headers: headers,
            body: JSON.stringify(
                {
                    "provider": "steam",
                    "token": steam_token,
                    "redirect": redirect_url
                }
            ),
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
