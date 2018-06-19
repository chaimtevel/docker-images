FROM rethinkdb:2.3.6

RUN apt-get -y  update && apt-get install -y  python python-pip && pip install rethinkdb

ENTRYPOINT ["rethinkdb", "dump"]