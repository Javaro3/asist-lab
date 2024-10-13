import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import FriendModal from './FriendModal';

const Header = ({ setIsAuthenticated }) => {
  const navigate = useNavigate();
  const [isModalOpen, setIsModalOpen] = useState(false);

  const handleLogout = () => {
    localStorage.removeItem('jwtToken');
    setIsAuthenticated(false);
    navigate('/login');
  };

  const handleOpenModal = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
  };

  return (
    <header className="bg-gray-800 text-white p-4">
      <nav className="flex justify-between items-center">
        <ul className="flex space-x-4">
          <li>
            <Link to="/main" className="hover:underline">
              My Trips
            </Link>
          </li>
          <li>
            <Link to="/add" className="hover:underline">
              Add Trip
            </Link>
          </li>
          <li>
            <Link to="/history" className="hover:underline">
              My History
            </Link>
          </li>
          <li>
            <Link to="/comments" className="hover:underline">
              Comments
            </Link>
          </li>
          <li>
            <Link to="/statistics" className="hover:underline">
              Statistics
            </Link>
          </li>
        </ul>
        <div className="flex space-x-4">
          <button
            onClick={handleOpenModal}
            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
          >
            Add Friend
          </button>
          <button
            onClick={handleLogout}
            className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600"
          >
            Logout
          </button>
        </div>
      </nav>

      <FriendModal
        isModalOpen={isModalOpen}
        closeModal={handleCloseModal}
      />
    </header>
  );
};

export default Header;
