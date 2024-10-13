import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import LoginPage from './components/Pages/LoginPage';
import RegisterPage from './components/Pages/RegisterPage';
import MainPage from './components/Pages/MainPage';
import AddPage from './components/Pages/AddPage';
import StatisticPage from './components/Pages/StatisticPage';
import HistoryPage from './components/Pages/HistoryPage';
import Modal from 'react-modal';
import CommentPage from './components/Pages/CommentPage';
import EditPage from './components/Pages/EditPage';

const App = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    Modal.setAppElement('#root');
    const token = localStorage.getItem('jwtToken');
    if (token) {
      setIsAuthenticated(true);
    }
  }, []);

  return (
    <Router>
      <Routes>
        {!isAuthenticated ? (
          <Route path="*" element={<Navigate to="/login" />} />
        ) : <></>}
        <Route path="/main" element={<MainPage setIsAuthenticated={setIsAuthenticated} />} />    
        <Route path="/login" element={<LoginPage setIsAuthenticated={setIsAuthenticated} />} />
        <Route path="/register" element={<RegisterPage setIsAuthenticated={setIsAuthenticated} />} />
        <Route path="/add" element={<AddPage setIsAuthenticated={setIsAuthenticated} />} />
        <Route path="/edit" element={<EditPage setIsAuthenticated={setIsAuthenticated} />} />
        <Route path="/history" element={<HistoryPage setIsAuthenticated={setIsAuthenticated} />} />
        <Route path="/comments" element={<CommentPage setIsAuthenticated={setIsAuthenticated} />} />
        <Route path="/statistics" element={<StatisticPage setIsAuthenticated={setIsAuthenticated} />} />
      </Routes>
    </Router>
  );
}

export default App;
