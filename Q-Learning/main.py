import requests
import numpy as np
import random


URL = "http://127.0.0.1:5000/Server"

Q = np.zeros((25, 4))
y = 0.95
eps = 0.5
lr = 0.8

decay_factor = 0.999

state = requests.get(URL + "?source=AI").json()
s = int(state["state"])

while True:
    # print(requests.get(URL + "?source=AI").json())
    if np.random.random() < eps or np.sum(Q[s, :]) == 0:
        a = np.random.randint(0, 4)
    else:
        a = np.argmax(Q[s, :])
    
    json = {
        "source": "AI",
        "state": s,
        "reward": state["reward"],
        "finished": state["finished"],
        "action": a
    }
    # print(a)
    post_response = requests.post(URL, json).json()
    print(post_response);
    new_state = requests.get(URL + "?source=AI").json()
    new_s = int(state["state"])


    Q[s, a] += int(new_state["reward"]) + lr * (y * np.max(Q[new_s, :]))
    state = new_state
    s = new_s