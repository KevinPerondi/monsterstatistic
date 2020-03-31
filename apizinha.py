# -*- coding: utf-8 -*-

import psycopg2
import json
from pathlib import Path
from flask import Flask
from flask import Response

class Historico:
    def __init__(self, boss, killed):
        self.Boss = boss
        self.Killed = killed

    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, sort_keys=True, indent=4)

app = Flask(__name__)

@app.route("/index")
@app.route("/")
def index():
    return "API dos boss de belobra."

@app.route("/historico")
@app.route("/historico/<bossname>")
def historico(bossname=None):
    conn = GetDatabaseConnection()
    cursor = conn.cursor()
    if bossname:
        return Response(GetBoss(cursor, bossname, toJson=True), mimetype="application/json")
    else:
        return Response(GetAll(cursor, toJson=True), mimetype="application/json")

def GetDatabaseConnection():
    try:
        configJson = ReadConfig()
        conn = psycopg2.connect("dbname='"+configJson["database"]+"' user='"+configJson["user"]+"' host='"+configJson["host"]+"' password='"+configJson["passwd"]+"'")
        return conn
    except Exception as e:
        print("Connection database failed.")

def CloseConnection(connection):
	try:
		connection.close()
	except Exception as e:
		print("Connection Close Failed")

def ReadConfig():
	p = Path(str(Path().absolute())+"/config.json")
	if p.exists():
		with open(str(p.resolve())) as fr:
			return json.load(fr)
	else:
		return None

def GetAll(cursor, toJson=True):
    try:
        cursor.execute("select bossname as Boss, to_char(killeddate,'dd/mm/YYYY') Killed from historico order by killeddate desc;")
        historico = cursor.fetchall()
        listaHistorico = []
        for item in historico:
            novo = Historico(boss=item[0],killed=item[1])
            listaHistorico.append(novo)
        if toJson:
            jsons = []
            for i in listaHistorico:
                jsons.append(i.toJSON())
            return json.dumps([ob.__dict__ for ob in listaHistorico])
        else:
            return listaHistorico
    except Exception as e:
        print("get all failed!" + str(e))
        return None

def GetBoss(cursor, bossname, toJson=True):
    try:
        print bossname
        cursor.execute("select bossname as Boss, to_char(killeddate,'dd/mm/YYYY') Killed from historico where lower(bossname) like '"+bossname.lower()+"%' order by killeddate desc;")
        historico = cursor.fetchall()
        listaHistorico = []
        for item in historico:
            novo = Historico(boss=item[0],killed=item[1])
            listaHistorico.append(novo)
        if toJson:
            jsons = []
            for i in listaHistorico:
                jsons.append(i.toJSON())
            return json.dumps([ob.__dict__ for ob in listaHistorico])
        else:
            return listaHistorico
    except Exception as e:
        print("get boss failed!" + str(e))
        return None

if __name__ == "__main__":
    app.run()