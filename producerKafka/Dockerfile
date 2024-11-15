FROM python:3.9-slim
WORKDIR /app
COPY requirements.txt .
RUN pip install -r requirements.txt
COPY . .
ENV DOCKER_ENV=true
CMD ["python", "producerKafka.py"]