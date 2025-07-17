import React, { useEffect, useRef, useState } from 'react';
import './RegistrationCard.css';
import LoginWithGoogleButton from './LoginWithGoogleButton/LoginWithGoogleButton';
import { popNotification } from '../../utils/popNotification';

interface InputGroupProps {
    labelText: string;
    inputName: string;
    inputType: string;
    dissolverPadding?: number;
    required?: boolean;
    value?: string;
    onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

function InputGroup({ labelText, inputName, inputType, dissolverPadding = 0, required, value, onChange, }: InputGroupProps) {
    const labelRef = useRef<HTMLLabelElement | null>(null);
    const inputRef = useRef<HTMLInputElement | null>(null);
    const [labelWidth, setLabelWidth] = useState(0);
    const [isFocused, setIsFocused] = useState(false);

    useEffect(() => {
        if (!labelRef.current) return;

        const resizeObserver = new ResizeObserver(entries => {
            for (let entry of entries) {
                setLabelWidth(entry.contentRect.width);
            }
        });

        resizeObserver.observe(labelRef.current);

        return () => resizeObserver.disconnect();
    }, []);

    const handleFocus = () => setIsFocused(true);
    const handleBlur = () => setIsFocused(false);

    return (
        <div className="relative">
            <input
                required={required}
                ref={inputRef}
                name={inputName}
                type={inputType}
                className="input-field rounded-[10px] border-[2px] border-[rgb(180,180,180)] text-center text-[rgb(180,180,180)] font-Kanit w-[100%] h-[64px]"
                onFocus={handleFocus}
                onBlur={handleBlur}
                value={value}
                onChange={onChange}
            />
            <label
                ref={labelRef}
                htmlFor={inputName}
                className="label absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 text-[rgb(180,180,180)] pointer-events-none z-[2] duration-[0.5s] font-Kanit text-[24px]"
                style={{ top: isFocused || inputRef.current?.value ? '0px' : '50%'}}
            >
                {labelText}
            </label>
            <div
                className="lineDissolver absolute bg-[rgb(255,255,255)] h-[2px] top-[0px] left-1/2 -translate-x-1/2 z-[1] duration-[0.3s] ease-in-out"
                style={{ width: isFocused || inputRef.current?.value ? `${labelWidth + dissolverPadding}px` : '0px' }}
            ></div>
        </div>
    );
}

const RegistrationCard: React.FC = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            const res = await fetch("http://localhost:5265/api/TestAuth/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email, password }),
            });

            if (!res.ok) {
                // Ошибка от сервера
                const errorText = await res.text();
                popNotification(`Error: ${errorText}`, 'error');
                return;
            }

            const data = await res.text(); // или json(), если возвращаешь json
            popNotification(`Success: ${data}`, 'success');
            // после успешной регистрации
            localStorage.setItem("currentUser", email);
            window.location.reload();
        } catch (error) {
            popNotification(`Ошибка при регистрации: ${error instanceof Error ? error.message : 'Неизвестная ошибка'}`, 'error');
        }
    };


    return (
        <div className="AuthenticationCard bg-[rgb(255,255,255)] pl-[40px] pr-[40px] pt-[20px] pb-[20px]  rounded-[20px] w-[100%] max-w-[512px]">
            <p className="font-Kanit text-[32px] mb-[20px]">Registration</p>
            <form className="flex flex-col" onSubmit={handleSubmit}>
                <div className="input-fields flex flex-col gap-[16px]">
                    <InputGroup labelText="E-Mail" inputName="emailInput" inputType="email" required dissolverPadding={20} value={email} onChange={e => setEmail(e.target.value)} />
                    <InputGroup labelText="Password" inputName="passwordInput" inputType="password" required dissolverPadding={20} value={password} onChange={e => setPassword(e.target.value)} />
                </div>

                <div className="w-[100%] flex">
                    <div className="ml-[10px] font-Kanit text-[rgb(180,180,180)] leading-none pt-[2px] pb-[6px]">
                        Forgot password? <span className="cursor-pointer hover:underline text-[rgb(120,120,120)]">Reset</span>
                    </div>
                </div>

                <input
                    type="submit"
                    value="Submit"
                    className="rounded-[10px] bg-[rgb(60,60,60)] text-[rgb(180,180,180)] font-Kanit h-[64px] text-[24px] hover:bg-[rgb(70,70,70)] cursor-pointer"
                />
            </form>
            <div className="relative">
                <div className="relative h-[2px] w-[100%] bg-[rgb(180,180,180)] mt-[20px]"></div>
                <p className="absolute top-[0px] left-[50%] -translate-x-1/2 -translate-y-1/2 text-[rgb(180,180,180)] bg-[rgb(255,255,255)] pl-[4px] pr-[4px] leading-none">or</p>
            </div>
            <LoginWithGoogleButton />

            <div className="font-Kanit text-[rgb(180,180,180)] leading-none pt-[4px]">
                Already have an account? <span  className="cursor-pointer hover:underline text-[rgb(120,120,120)]">Login</span>
            </div>
        </div>
    );
};

export default RegistrationCard;

