import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import InputField from '../UI/InputField';
import ErrorMessage from '../UI/ErrorMessage';
import Button from '../UI/Button';
import useAuth from '../../hooks/useAuth';

const LoginPage = ({ setIsAuthenticated }) => {
    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');
    const { error, handleAuth } = useAuth(false);
    const navigate = useNavigate();
    
    const handleLogin = (e) => {
        e.preventDefault();
        handleAuth(login, password, setIsAuthenticated);
        navigate('/login')
    };

    return (
        <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100">
        <form onSubmit={handleLogin} className="bg-white p-8 rounded shadow-md">
            <h2 className="text-2xl mb-4">Login</h2>
            <ErrorMessage message={error} />
            <InputField
                type="text"
                placeholder="Login"
                value={login}
                onChange={(e) => setLogin(e.target.value)}
            />
            <InputField
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />
            <Button type="submit" className="bg-blue-500 text-white">
                Log In
            </Button>
            <p className="mt-4">
                Don't have an account?{' '}
            <Link to="/register" className="text-blue-500 underline">
                Register
            </Link>
            </p>
        </form>
        </div>
    );
};

export default LoginPage;
