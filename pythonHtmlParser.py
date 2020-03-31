# -*- coding: utf-8 -*-

from pathlib import Path
import re
import psycopg2
import json

def RetornaListaMobs(mobFile):
	mobs = []
	with open(mobFile, "r") as fd:
		lines = fd.read().splitlines()
		for line in lines:
			mobs.append(line)
	return mobs

def RetornaEncontrados(mobsList, htmlPath):
	dicionarioDosMortosOntem = []
	with open(htmlPath,"r") as f:
		texto = f.read()
		encontrado = re.search("(<TR BG).*(<\/TR>)",texto)
		textoFiltrado = encontrado.group(0)
		textoFiltrado = re.sub("\<.*?\>", "", textoFiltrado)
		textoFiltrado = textoFiltrado.replace("&#160","")
		textoFiltrado = textoFiltrado.lower()
		tudoSplitado = textoFiltrado.split(";")#aqui tem a lista com o nome e os valores
		for	mob in mobsList:
			if mob.lower() in tudoSplitado:
				mobIndex = tudoSplitado.index(mob.lower())
				if mobIndex > 0 and int(tudoSplitado[mobIndex+2]) > 0:
					dicionarioDosMortosOntem.append(mob)
	return dicionarioDosMortosOntem

def GetDatabaseConnection(configJson):
	try:
		conn = psycopg2.connect("dbname='"+configJson["database"]+"' user='"+configJson["user"]+"' host='"+configJson["host"]+"' password='"+configJson["passwd"]+"'")
		return conn
	except Exception as e:
		print("Connection database failed.")

def CloseConnection(connection):
	try:
		connection.close()
	except Exception as e:
		print("Connection Close Failed")

def saveOnDatabase(cursor, mobList):
	for mob in mobList:
		try:
			if("'" in mob):
				mob = mob.replace("'","")
			sql = "insert into historico (bossname,killeddate,insertiondate, server) values ('"+mob+"',current_date-1, current_date, 'Belobra')"
			cursor.execute(sql)
		except Exception as e:
			print('Error on saving result. [Method: saveOnDatabase]')
			print(str(e))

def ReadConfig():
	p = Path(str(Path().absolute())+"/config.json")
	if p.exists():
		with open(str(p.resolve())) as fr:
			return json.load(fr)
	else:
		return None

if __name__ == "__main__":
	mobsPath = str(Path().absolute())+"/mobs.txt"
	htmlPath = str(Path().absolute())+"/otp"
	dbConfigs = ReadConfig()
	mobs = RetornaListaMobs(mobsPath)
	deramOntem = RetornaEncontrados(mobs,htmlPath)
	dbConnection = GetDatabaseConnection(dbConfigs)
	cursorDB = dbConnection.cursor()
	saveOnDatabase(cursorDB,deramOntem)
	dbConnection.commit()
	CloseConnection(dbConnection)
