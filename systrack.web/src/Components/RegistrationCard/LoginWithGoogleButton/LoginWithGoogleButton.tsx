
import React from "react";

const LoginWithGoogleButton = () => {
    const handleLogin = () => {
        window.location.href = "http://localhost:3000/signin-google";
    };

    return (
        <button
            onClick={handleLogin}
            className="bg-white flex justify-center items-center border-[2px] border-[rgb(180,180,180)] rounded-[10px] hover:shadow-md transition h-[64px] w-[100%] mt-[16px]"
        >
            <img src="https://developers.google.com/identity/images/g-logo.png" alt="Google" className="h-[32px] w-[32px]" />
        </button>
    );
};

export default LoginWithGoogleButton;
