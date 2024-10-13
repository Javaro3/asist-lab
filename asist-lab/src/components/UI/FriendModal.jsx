import React, { useEffect, useState } from 'react';
import Modal from 'react-modal';

const FriendModal = ({ isModalOpen, closeModal }) => {
  const [friends, setFriends] = useState([]);

  const fetchFriends = async () => {
    const response = await fetch('https://localhost:7213/User/get-all', {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('jwtToken')}`,
      }
    });

    if (response.ok) {
      const data = await response.json();
      setFriends(data);
    }
  };

  useEffect(() => {
    fetchFriends();
  }, []);

  const handleAddFriend = async () => {
    const friendId = document.getElementById("friendSelect").value;
    const response = await fetch("https://localhost:7213/User/add-friend", {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('jwtToken')}`,
      },
      body: JSON.stringify(friendId)
    });

    if (response.ok) {
        closeModal();
    }
  };

  return (
    <Modal
      isOpen={isModalOpen}
      onRequestClose={closeModal}
      style={{
        overlay: {
          zIndex: 1000,
        },
        content: {
          top: '50%',
          left: '50%',
          right: 'auto',
          bottom: 'auto',
          marginRight: '-50%',
          transform: 'translate(-50%, -50%)',
          width: '400px',
          padding: '20px',
          zIndex: 1000,
        },
      }}
    >
      <h2 className="text-lg mb-4">Add a Friend</h2>
      <div className="flex flex-col gap-4">
        <select
          id="friendSelect"
          className="w-full p-2 border border-gray-300 rounded"
        >
          {friends.map((friend) => (
            <option key={friend.id} value={friend.id}>
              {friend.login}
            </option>
          ))}
        </select>
        <div className="flex justify-end mt-4">
          <button
            onClick={handleAddFriend}
            className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 mr-2"
          >
            Add Friend
          </button>
        </div>
      </div>
    </Modal>
  );
};

export default FriendModal;
