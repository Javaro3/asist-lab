import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import InputField from '../UI/InputField';
import ErrorMessage from '../UI/ErrorMessage';
import Button from '../UI/Button';
import useAuth from '../../hooks/useAuth';

const RegisterPage = ({ setIsAuthenticated }) => {
    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');
    const { error, handleAuth } = useAuth(true);
    
    const handleRegister = (e) => {
      e.preventDefault();
      handleAuth(login, password, setIsAuthenticated);
    };

    return (
        <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100">
        <form onSubmit={handleRegister} className="bg-white p-8 rounded shadow-md">
            <h2 className="text-2xl mb-4">Register</h2>
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
            <Button type="submit" className="bg-green-500 text-white">
                Register
            </Button>
            <p className="mt-4">
                Already have an account?{' '}
            <Link to="/login" className="text-blue-500 underline">
                Login
            </Link>
            </p>
        </form>
        </div>
    );
};

export default RegisterPage;
