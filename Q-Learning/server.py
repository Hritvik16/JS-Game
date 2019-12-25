from flask import Flask
from flask_restful import Api, Resource, reqparse

import numpy as np
import random

app = Flask(__name__)
api = Api(app)

class Model(Resource):
    current_action = {
    "action": -1
};
    current_state = {
    "state": None,
    "reward": None,
    "finished": None
}
    def post(self):
        parser = reqparse.RequestParser()
        parser.add_argument("source")
        parser.add_argument("state")
        parser.add_argument("reward")
        parser.add_argument("finished")

        parser.add_argument("action")
        args = parser.parse_args()

        if(args["source"] == "AI"):
            self.current_action["action"] = args["action"]
            print("AI HIT")
            print(self.current_action["action"])
            return self.current_action["action"], 201
        else:
            self.current_state["state"] = args["state"]
            self.current_state["reward"] = args["reward"]
            self.current_state["finished"] = args["finished"]
            return self.current_state, 201
        

    def get(self):
        parser = reqparse.RequestParser()
        parser.add_argument("source")
        args = parser.parse_args()
        if(args["source"] == "AI"):
            return self.current_state
        else:
            print(self.current_action)
            print("""                 """)
            print("""                 """)
            print("""                 """)
            print("""                 """)
            return self.current_action["action"]

api.add_resource(Model, "/Server")
app.run(debug=True)