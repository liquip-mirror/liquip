#!/bin/env python3

import ctypes, os

try:
    is_admin = os.getuid() == 0
except AttributeError:
    is_admin = ctypes.windll.shell32.IsUserAnAdmin() != 0

# if is_admin:
#     print("DO NOT RUN AS ADMIN/ROOT")
#     exit(1)


import json, io, os

from os import path
from typing import Optional


config_file = open('cosmos.json', 'r')

config = json.load(config_file)


root = path.join(os.getcwd(), "cosmos")
patches = path.join(os.getcwd(), "Patches")

if path.exists(root) is False:
    os.mkdir(root)

os.chdir(root)

print(root)

class Config():

    def __init__(self, data):
        self.name = data['name']
        self.uri = data['uri']
        self.branch = data['branch']
        self.commit = data['commit']

    name: str
    uri: str
    branch: Optional[str]
    commit: Optional[str]

def GetConfig(string: str) -> Optional[Config]:
    for i in config['repos']:
        if i['name'] == string:
            return Config(i)
    return None


def GitClone(name: str):
    os.chdir(root)
    c = GetConfig(name)
    if path.exists(path.join(root, c.name)) is False:
        os.system("git clone {0} {1}".format(c.uri, c.name))


def GitPull(name: str):
    os.chdir(root)
    c = GetConfig(name)
    os.chdir(path.join(root, c.name))
    os.system("git pull")
    os.system("git checkout {0}".format(c.branch or c.commit))


def GitPatch(name: str):
    os.chdir(root)
    c = GetConfig(name)
    if path.exists(path.join(patches, c.name)):
        print("apply patches")
        for patch in sorted(os.listdir(path.join(patches, c.name))):
            patch_path = os.chdir(path.join(patches, c.name))
            os.system("git apply {0}".format(patch_path))
    else:
        print("no patches")

for i in config['repos']:
    print(i)
    GitClone(i['name'])
    GitPull(i['name'])
    GitPatch(i['name'])

import platform

os.chdir(path.join(root, "Cosmos"))

os_name = platform.system().lower()

print(os_name)

if os_name == "linux":
    os.system("make build")
    os.system("make publish")
    os.system("{0} make install".format("" if is_admin else "sudo"))
    os.system("make nuget-install")
elif os_name == "darwin":
    print("not supported")
elif os_name == "win32":
    os.system("./install-VS2022.bat")

