import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const useAuth = (isRegister = false) => {
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleAuth = async (login, password, setIsAuthenticated) => {
    setError('');

    if (!login) {
      setError('Login field is required');
      return;
    }

    if (!password) {
      setError('Password field is required');
      return;
    }

    const response = await fetch(`https://localhost:7213/Auth/${isRegister ? 'register' : 'login'}`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ login, password }),
    });

    if (response.ok) {
      const data = await response.json();
      localStorage.setItem('jwtToken', data.token);
      setIsAuthenticated(true);
      navigate('/main');
    } else {
      const errorData = await response.json();
      setError(errorData.message || 'Authentication failed. Please try again.');
    }
  };

  return { error, handleAuth };
};

export default useAuth;
